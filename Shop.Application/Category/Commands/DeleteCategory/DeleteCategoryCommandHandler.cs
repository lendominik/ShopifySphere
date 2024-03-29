﻿using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccessControlService _accessControlService;
        private readonly IUserContext _userContext;

        public DeleteCategoryCommandHandler(IAccessControlService accessControlService, ICategoryRepository categoryRepository, IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _accessControlService = accessControlService;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_accessControlService.IsEditable(_userContext))
            {
                return Unit.Value;
            }

            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }
            
            await _categoryRepository.Delete(category);

            return Unit.Value;
        }
    }
}
