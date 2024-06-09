namespace habitsBuilderBackEnd.Models
{
    public class HabitCard
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<ChecklistItem> ChecklistItems { get; set; }
    }
}
