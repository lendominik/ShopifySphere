namespace Shop.Application.Services
{
    public interface IPaginationService
    {
        IEnumerable<T> PaginationSkipAndTake<T>(IQueryable<T> objects, int pageNumber, int pageSize);
    }

    public class PaginationService : IPaginationService
    {
        public IEnumerable<T> PaginationSkipAndTake<T>(IQueryable<T> objects, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var objectsToDisplay = objects.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return objectsToDisplay;
        }
    }
}
