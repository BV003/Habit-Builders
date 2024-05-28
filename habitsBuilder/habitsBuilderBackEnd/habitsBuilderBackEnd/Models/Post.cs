namespace habitsBuilderBackEnd.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostedAt { get; set; }
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
    }
    public class PostLike
    {
        public int PostLikeId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
