﻿using FluentValidation;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator(IItemRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3)
                .Custom((value, context) =>
                {
                    var existingItem = repository.GetByName(value).Result;
                    if (existingItem != null)
                    {
                        context.AddFailure("Przedmiot o tej nazwie istnieje już w bazie danych.");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(c => c.Price)
                .NotEmpty();

            RuleFor(c => c.StockQuantity)
                .NotEmpty();

            RuleFor(c => c.CategoryEncodedName)
                .NotEmpty();

            RuleFor(c => c.ProductImage)
                .NotEmpty();
        }
    }
}