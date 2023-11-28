using MediatR;

namespace Shop.Application.Category.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryDto>
    {
        public string EncodedName { get; set; }
        public GetCategoryQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
