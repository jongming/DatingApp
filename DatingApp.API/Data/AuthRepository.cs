using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            Console.WriteLine("***AuthRepository.cs: Login() " + DateTime.Now.ToString());
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;

            return user;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            Console.WriteLine("***AuthRepository.cs: VerifyPasswordHash() " + DateTime.Now.ToString());
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0; i < computedHash.Length; i++){
                    if (computedHash[i] != computedHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            Console.WriteLine("***AuthRepository.cs: Register() " + DateTime.Now.ToString());
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); //'out' set it as reference, not the actual value

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            Console.WriteLine("***AuthRepository.cs: CreatePasswordHash() " + DateTime.Now.ToString());
            //'using()' anything inside the () will be desposed once it is used.
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            Console.WriteLine("***AuthRepository.cs: UserExists() " + DateTime.Now.ToString());
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;    
        }
    }
}