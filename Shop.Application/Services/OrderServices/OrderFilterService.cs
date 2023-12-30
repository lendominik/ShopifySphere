using MediatR;

namespace Shop.Application.Services.OrderServices
{
    public interface IOrderFilterService
    {
        IQueryable<Domain.Entities.Order> FilterBySearchPhrase(IQueryable<Domain.Entities.Order> orders, string searchPhrase);
        IQueryable<Domain.Entities.Order> FilterByStatus(IQueryable<Domain.Entities.Order> orders, string orderStatus);
    }

    public class OrderFilterService : IOrderFilterService
    {
        public IQueryable<Domain.Entities.Order> FilterBySearchPhrase(IQueryable<Domain.Entities.Order> orders, string searchPhrase)
        {
            if (!string.IsNullOrEmpty(searchPhrase))
            {
                orders = orders.Where(r => r.FirstName.ToLower().Contains(searchPhrase.ToLower()) ||
                r.LastName.ToLower().Contains(searchPhrase.ToLower()) ||
                r.Email.ToLower().Contains(searchPhrase.ToLower()));
            }

            return orders;
        }
        public IQueryable<Domain.Entities.Order> FilterByStatus(IQueryable<Domain.Entities.Order> orders, string orderStatus)
        {
            orders = orders.Where(r => r.OrderStatus.ToString() == orderStatus);

            return orders;
        }
    }
}
