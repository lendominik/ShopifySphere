using Shop.Application.Exceptions;

namespace Shop.Application.Services
{
    public class DataProcessingService
    {
        public T ProcessData<T>(T request)
        {
            request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize < 1 ? 10 : request.PageSize;

            if (data == null || data.Count == 0)
            {
                throw new NotFoundException("Data not found.");
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                var properties = typeof(T).GetProperties()
                                          .Where(prop => prop.PropertyType == typeof(string))
                                          .ToList();

                data = data.Where(item => properties.Any(prop =>
                          prop.GetValue(item)?.ToString().ToLower().Contains(request.SearchPhrase.ToLower()) == true))
                          .ToList();
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                data = data.Where(item => item.ToString() == request.Status).ToList();
            }

            // Załóżmy, że sortowanie będzie wykonywane na podstawie właściwości "OrderDate"
            data = data.OrderByDescending(item =>
                    typeof(T).GetProperty("OrderDate")?.GetValue(item) ?? DateTime.MinValue)
                    .ToList();

            return data;
        }
    }
}
