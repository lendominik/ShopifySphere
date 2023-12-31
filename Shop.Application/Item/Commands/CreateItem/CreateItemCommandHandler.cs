using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {

        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IAccessControlService _accessControlService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserContext _userContext;

        public CreateItemCommandHandler(IUserContext userContext, IWebHostEnvironment webHostEnvironment, IAccessControlService accessControlService, IFileService fileService, IItemRepository itemRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileService = fileService;
            _accessControlService = accessControlService;
            _webHostEnvironment = webHostEnvironment;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            if(!_accessControlService.IsEditable(_userContext))
            {
                return Unit.Value;
            }

            var imageName = _fileService.UploadFile(request.Image, _webHostEnvironment);

            var item = _mapper.Map<Domain.Entities.Item>(request);

            item.ProductImage = imageName;

            var category = await _categoryRepository.GetByEncodedName(request.CategoryEncodedName!);

            if(category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            item.EncodeName();
            item.Category = category;

            await _itemRepository.Create(item);

            return Unit.Value;
        }
    }
}
