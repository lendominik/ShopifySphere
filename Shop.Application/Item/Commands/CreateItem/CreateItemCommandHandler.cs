using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IWebHostEnvironment webHostEnvironment, IItemRepository itemRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var imageName = UploadFile(request.Image);

            var item = _mapper.Map<Domain.Entities.Item>(request);

            item.ProductImage = imageName;

            var category = await _categoryRepository.GetByEncodedName(request.CategoryEncodedName!);

            if(category == null)
            {
                throw new NotFoundException("Podana kategoria nie istnieje.");
            }

            item.EncodeName();
            item.Category = category;

            await _itemRepository.Create(item);

            return Unit.Value;
        }

        private string UploadFile(IFormFile image)
        {
            string fileName = null;

            if(image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                } 
            }
            return fileName;
        }
    }
}
