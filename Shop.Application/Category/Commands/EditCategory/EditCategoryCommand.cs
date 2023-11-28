using MediatR;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommand : IRequest
    {
        public string Description { get; set; }
        public string EncodedName { get; set; }
    }
}
