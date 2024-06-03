namespace habitsBuilderBackEnd.DTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public int Likes { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
