namespace habitsBuilderBackEnd.Models
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool Status { get; set; }
        public int Days { get; set; }
        public int CardId { get; set; }
        public HabitCard Card { get; set; }
    }
}
