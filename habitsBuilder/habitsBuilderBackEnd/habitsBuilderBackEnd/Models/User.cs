

using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace habitsBuilderBackEnd.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<UserFriend> Friends { get; set; } = new List<UserFriend>();
        public List<Post> Posts { get; set; } = new List<Post>();


        public User(string userId)
        {
            this.UserId = userId;
        }
    
        public User()
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is User other)
            {
                return UserId == other.UserId;
            }
            return false;
        }
    
    }
    
}
