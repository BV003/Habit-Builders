using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

namespace habitsBuilderBackEnd.Services
{
    public class UserService
    {
        private readonly RecordDbContext _context;

        public UserService(RecordDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
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
                user.Password = newPassword;
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
    }
}
