using System;
using System.Threading.Tasks;
using Nviromon.Models;
using Microsoft.EntityFrameworkCore;

namespace Nviromon.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataService dataService;

        public AuthRepository(DataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<User> register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.lastLogin = DateTime.Now;
            user.dateCreated = DateTime.Now;
            user.Id = Guid.NewGuid();

            await dataService.Users.AddAsync(user);
            await dataService.SaveChangesAsync();

            return user;
        }

        public async Task<User> login(string username, string password)
        {
            var user = await dataService.Users.FirstOrDefaultAsync(x => x.username == username);
            if( user == null )
            {
                return null;
            }

            if(!verifyPasswordHash(password, user.passwordHash, user.passwordSalt))
            {
                return null;
            }

            return user;
        }
        

        public async Task<bool> userExists(string username)
        {
            if (await dataService.Users.AnyAsync(x => x.username == username))
            {
                return true;
            }

            return false;
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt =  hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i<computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }   
    }
}