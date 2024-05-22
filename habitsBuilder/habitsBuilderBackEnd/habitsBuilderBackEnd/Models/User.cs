namespace habitsBuilderBackEnd.Models
{
        public class User
        {
            public string userId { get; set; }
            public User(string userId)
            {
                this.userId = userId;
            }

            public User()
            {
            }
            public override bool Equals(object obj)
            {
                if (obj is User other)
                {
                    return userId == other.userId;
                }
                return false;
            }

        }
    
}
