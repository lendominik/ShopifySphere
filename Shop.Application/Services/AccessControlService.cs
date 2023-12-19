using Shop.Application.ApplicationUser;
using Shop.Application.Order.Commands.ShipOrder;

namespace Shop.Application.Services
{
    public interface IAccessControlService
    {
        bool IsEditable(IUserContext userContext);
    }

    public class AccessControlService : IAccessControlService
    {
        public bool IsEditable(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();
            return user != null && user.IsInRole("Owner");
        }
    }
}
