using System.Text.Json.Serialization;

namespace habitsBuilderBackEnd.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Friends { get; set; } = new List<string>();

        [JsonIgnore]
        public List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}
