using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

namespace habitsBuilderBackEnd.Services
{
    public class PostService
    {
        private readonly RecordDbContext _context;

        public PostService(RecordDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePostAsync(string userId, string content, string imageUrl)
        {
            var post = new Post
            {
                UserId = userId,
                Content = content,
                ImageUrl = imageUrl,
                PostedAt = DateTime.Now
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> LikePostAsync(int postId, string userId)
        {
            var post = await _context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.PostId == postId);
            var user = await _context.Users.FindAsync(userId);

            if (post == null || user == null)
            {
                return false;
            }

            if (post.Likes.Any(l => l.UserId == userId))
            {
                return false; // 用户已经点赞过这个帖子
            }

            post.Likes.Add(new PostLike { PostId = postId, UserId = userId });
            await _context.SaveChangesAsync();

            return true;
        }
        // 获取用户自己发布的所有帖子
        public async Task<List<PostDTO>> GetUserPostsAsync(string userId)
        {
            var posts = await _context.Posts
                .Where(p => p.UserId == userId)
                .Select(p => new PostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    ImageUrl = p.ImageUrl,
                    PostedAt = p.PostedAt,
                    Likes = p.Likes.Count
                })
                .ToListAsync();

            return posts;
        }

        // 获取用户及其好友发布的所有帖子
        public async Task<List<PostDTO>> GetUserAndFriendsPostsAsync(string userId)
        {
            // 获取用户的所有好友ID
            var friendsIds = await _context.UserFriends
                .Where(uf => uf.UserId == userId)
                .Select(uf => uf.FriendId)
                .ToListAsync();

            // 加上用户自己的ID
            friendsIds.Add(userId);

            var posts = await _context.Posts
                .Where(p => friendsIds.Contains(p.UserId))
                .Select(p => new PostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    ImageUrl = p.ImageUrl,
                    PostedAt = p.PostedAt,
                    Likes = p.Likes.Count
                })
                .ToListAsync();

            return posts;
        }
    }
}
