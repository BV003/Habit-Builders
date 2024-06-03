using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace habitsBuilderBackEnd.Controllers
{
    [Route("api/user/posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService postService;
        public PostController(PostService postService)
        {
            this.postService = postService;
        }


        [HttpPost("{id}/posts")]
        public async Task<ActionResult<Post>> CreatePost(string id, [FromForm] CreatePostRequest request)
        {
            var post = await postService.CreatePostAsync(id, request.Content, request.Files ?? new List<IFormFile>());
            return Ok(new { message = "创造帖子成功" });
        }

        [HttpPost("posts/{postId}/like")]
        public async Task<IActionResult> LikePost(int postId, [FromBody] LikePostRequest request)
        {
            var result = await postService.LikePostAsync(postId, request.UserId);
            if (!result)
            {
                return Ok(new { message = "用户已经点赞过这个帖子" });
            }
            return Ok(new { message = "点赞成功" });
        }

        [HttpPost("posts/{postId}/unlike")]
        public async Task<IActionResult> UnlikePost(int postId, [FromBody] LikePostRequest request)
        {
            var result = await postService.UnlikePostAsync(postId, request.UserId);
            if (!result)
            {
                return Ok(new { message = "用户没有点赞这个帖子" });
            }
            return Ok(new { message = "取消点赞成功" });
        }

        [HttpGet("posts/{postId}/hasLiked")]
        public async Task<IActionResult> HasLikedPost(int postId, [FromQuery] string userId)
        {
            var result = await postService.HasLikedPostAsync(postId, userId);
            if (result)
            {
                return Ok(new { hasLiked = true });
            }
            return Ok(new { hasLiked = false });
        }

        // 获取用户自己发布的所有帖子
        [HttpGet("{id}/posts")]
        public async Task<ActionResult<List<PostDTO>>> GetUserPosts(string id)
        {
            var posts = await postService.GetUserPostsAsync(id);
            if (posts == null || posts.Count == 0)
            {
                return Ok(new { message = "用户没有发布过帖子" });
            }
            return posts;
        }

        // 获取用户及其好友发布的所有帖子
        [HttpGet("{id}/allposts")]
        public async Task<ActionResult<List<PostDTO>>> GetUserAndFriendsPosts(string id)
        {
            var posts = await postService.GetUserAndFriendsPostsAsync(id);
            if (posts == null || posts.Count == 0)
            {
                return Ok(new { message = "用户和好友没有发布过帖子" });
            }
            return posts;
        }
        // 删除相关帖子
        [HttpDelete("{userId}/posts/{postId}")]
        public async Task<ActionResult> DeletePost(string userId, int postId)
        {
            var result = await postService.DeletePostAsync(userId, postId);
            if (!result)
            {
                return Ok(new { message = "帖子不存在或不属于该用户" });
            }
            return Ok(new { message = "帖子删除成功" });
        }
    }

    public class CreatePostRequest
    {
        public string Content { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>(); // 确保Files列表被初始化
    }

    public class LikePostRequest
    {
        public string UserId { get; set; }
    }
}
