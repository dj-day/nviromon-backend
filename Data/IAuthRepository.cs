using System.Threading.Tasks;
using Nviromon.Models;

namespace Nviromon.Data
{
    public interface IAuthRepository
    {
         Task<User> register(User user, string password);

         Task<User> login(string username, string password);

         Task<bool> userExists(string username);
    }
}