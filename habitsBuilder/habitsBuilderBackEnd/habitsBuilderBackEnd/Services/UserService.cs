using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace habitsBuilderBackEnd.Services
{
    public class UserService
    {
        private readonly RecordDbContext _context;

        public UserService(RecordDbContext context)
        {
            _context = context;
        }

        public async Task<(User? User, string Message)> RegisterUserAsync(User user)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(user.UserId))
            {
                return (null, "用户名不能为空");
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return (null, "昵称不能为空");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return (null, "密码不能为空");
            }
            if (user.Password.Length < 6)
            {
                return (null, "密码长度不能少于6位");
            }

            // Check for duplicate user
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                return (null, "用户名已存在");
            }

            // Hash the password before saving
            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (user, "注册成功");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> UpdatePasswordAsync(string userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Password = HashPassword(newPassword);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<bool> AddFriendAsync(string userId, string friendId)
        {
            var user = await _context.Users.FindAsync(userId);
            var friend = await _context.Users.FindAsync(friendId);

            if (user == null || friend == null)
            {
                return false;
            }

            user.Friends.Add(new UserFriend { UserId = userId, FriendId = friendId });
            friend.Friends.Add(new UserFriend { UserId = friendId, FriendId = userId });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFriendAsync(string userId, string friendId)
        {
            var userFriend = await _context.UserFriends
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId);
            var userFriend2 = await _context.UserFriends
                .FirstOrDefaultAsync(uf => uf.UserId == friendId && uf.FriendId == userId);
            if (userFriend == null)
            {
                return false;
            }

            _context.UserFriends.Remove(userFriend);
            _context.UserFriends.Remove(userFriend2);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserDTO> GetUserDTOAsync(string userId)
        {
            var user = await GetUserAsync(userId);
            if (user == null) return null;

            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Friends = user.Friends.Select(f => f.FriendId).ToList()
            };

            return userDTO;
        }
        public async Task<User> ValidateUserAsync(string userId, string password)
        {
            // Hash the input password and compare with stored hash
            string hashedPassword = HashPassword(password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId && u.Password == hashedPassword);

            return user;
        }
    }
}
