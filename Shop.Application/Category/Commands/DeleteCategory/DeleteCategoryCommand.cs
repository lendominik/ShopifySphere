using MediatR;

namespace Shop.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public string EncodedName { get; set; }
    }
}
