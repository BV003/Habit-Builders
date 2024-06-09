using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

namespace habitsBuilderBackEnd.Services
{
    public class PostService
    {
        private readonly RecordDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostService(RecordDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<Post> CreatePostAsync(string userId, string content, List<IFormFile> files)
        {
            if (string.IsNullOrEmpty(_env.WebRootPath))
            {
                throw new InvalidOperationException("Web root path is not configured.");
            }
            var post = new Post
            {
                UserId = userId,
                Content = content,
                PostedAt = DateTime.Now,
                Photos = new List<PostPhoto>() // 初始化Photos列表
            };

            // 检查文件列表是否为空或包含零个文件
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                        var filePath = Path.Combine(_env.WebRootPath, "uploads", newFileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var photo = new PostPhoto
                        {
                            Url = $"/uploads/{newFileName}"
                        };

                        post.Photos.Add(photo);
                    }
                }
            }

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

        public async Task<bool> UnlikePostAsync(int postId, string userId)
        {
            var postLike = await _context.PostLikes
                .FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);

            if (postLike == null)
            {
                return false; // 用户没有点赞这个帖子
            }

            _context.PostLikes.Remove(postLike);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasLikedPostAsync(int postId, string userId)
        {
            return await _context.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);
        }

        // 获取用户自己发布的所有帖子
        public async Task<List<PostDTO>> GetUserPostsAsync(string userId)
        {
            var posts = await _context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Photos)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.PostedAt)
                .Select(p => new PostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    PostedAt = p.PostedAt,
                    Likes = p.Likes.Count,
                    Photos = p.Photos.Select(photo => photo.Url).ToList(),
                    UserId = p.UserId,
                    UserName = p.User.UserName // Assuming User entity has UserName property
                })
                .ToListAsync();

            return posts;
        }

        // 获取用户自己及其好友发布的所有帖子
        public async Task<List<PostDTO>> GetUserAndFriendsPostsAsync(string userId)
        {
            var friendsIds = await _context.UserFriends
                .Where(uf => uf.UserId == userId)
                .Select(uf => uf.FriendId)
                .ToListAsync();

            friendsIds.Add(userId);

            var posts = await _context.Posts
                .Where(p => friendsIds.Contains(p.UserId))
                .Include(p => p.Photos)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.PostedAt)
                .Select(p => new PostDTO
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    PostedAt = p.PostedAt,
                    Likes = p.Likes.Count,
                    Photos = p.Photos.Select(photo => photo.Url).ToList(),
                    UserId = p.UserId,
                    UserName = p.User.UserName // Assuming User entity has UserName property
                })
                .ToListAsync();

            return posts;
        }

        //删除帖子
        public async Task<bool> DeletePostAsync(string userId, int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Photos)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.PostId == postId && p.UserId == userId);

            if (post == null)
            {
                return false; // 帖子不存在或不属于该用户
            }

            // 删除相关帖子
            foreach (var photo in post.Photos)
            {
                var filePath = Path.Combine(_env.WebRootPath, photo.Url.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<PostDTO>> GetUserLikedPostsAsync(string userId)
        {
            var likedPosts = await _context.PostLikes
                .Where(pl => pl.UserId == userId)
                .Include(pl => pl.Post)
                .ThenInclude(p => p.Photos)
                .Include(pl => pl.Post)
                .ThenInclude(p => p.Likes)
                .Select(pl => new PostDTO
                {
                    PostId = pl.Post.PostId,
                    Content = pl.Post.Content,
                    PostedAt = pl.Post.PostedAt,
                    Likes = pl.Post.Likes.Count,
                    Photos = pl.Post.Photos.Select(photo => photo.Url).ToList(),
                    UserId = pl.Post.UserId,
                    UserName = pl.Post.User.UserName // Assuming User entity has UserName property
                })
                .ToListAsync();

            return likedPosts;
        }
    }
}
