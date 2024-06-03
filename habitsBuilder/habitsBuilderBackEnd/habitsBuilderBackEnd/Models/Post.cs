namespace habitsBuilderBackEnd.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } // 添加User导航属性
        public List<PostPhoto> Photos { get; set; }=new List<PostPhoto>();
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
    public class PostPhoto
    {
        public int PostPhotoId { get; set; }
        public int PostId { get; set; }
        public string Url { get; set; }
        public Post Post { get; set; }
    }
}
