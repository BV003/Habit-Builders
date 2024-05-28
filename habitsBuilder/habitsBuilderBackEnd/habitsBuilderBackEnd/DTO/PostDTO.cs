namespace habitsBuilderBackEnd.DTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostedAt { get; set; }
        public int Likes { get; set; }
    }
}
