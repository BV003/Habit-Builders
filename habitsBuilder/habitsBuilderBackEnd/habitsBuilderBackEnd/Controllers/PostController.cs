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
        public async Task<ActionResult<Post>> CreatePost(string id, [FromBody] CreatePostRequest request)
        {
            var post = await postService.CreatePostAsync(id, request.Content, request.ImageUrl);
            return Ok(new { message = "创造帖子成功" });
        }

        [HttpPost("posts/{postId}/like")]
        public async Task<IActionResult> LikePost(int postId, [FromBody] LikePostRequest request)
        {
            var result = await postService.LikePostAsync(postId, request.UserId);
            if (!result)
            {
                return BadRequest(new { message = "用户已经点赞过这个帖子" });
            }
            return Ok(new { message = "点赞成功" });
        }
        // 获取用户自己发布的所有帖子
        [HttpGet("{id}/posts")]
        public async Task<ActionResult<List<PostDTO>>> GetUserPosts(string id)
        {
            var posts = await postService.GetUserPostsAsync(id);
            if (posts == null || posts.Count == 0)
            {
                return NotFound(new { message = "用户没有发布过帖子" });
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
                return NotFound(new { message = "用户和好友没有发布过帖子" });
            }
            return posts;
        }
    }

    public class CreatePostRequest
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }

    public class LikePostRequest
    {
        public string UserId { get; set; }
    }
}
