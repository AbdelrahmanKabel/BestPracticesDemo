using System.Threading.Tasks;

namespace BestPracticesDemo.UI.Blazor.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(string firstName, string lastName, string userName, string email, string password);
        Task Logout();
    }
}
