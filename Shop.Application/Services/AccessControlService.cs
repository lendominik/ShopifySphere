using Shop.Application.ApplicationUser;

namespace Shop.Application.Services
{
    public interface IAccessControlService
    {
        bool IsEditable();
    }

    public class AccessControlService : IAccessControlService
    {
        private readonly IUserContext _userContext;

        public AccessControlService(IUserContext userContext)
        {
            _userContext = userContext;
        }
        public bool IsEditable()
        {
            var user = _userContext.GetCurrentUser();
            return user != null && user.IsInRole("Owner");
        }
    }
}
