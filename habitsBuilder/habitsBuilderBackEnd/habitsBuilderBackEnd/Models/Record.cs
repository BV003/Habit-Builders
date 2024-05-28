namespace habitsBuilderBackEnd.Models
{
    public class Record
    {
        public string RecordId { get; set; }
        public string UserId { get; set; }
        public DateTime dateTime { get; set; }
        public int sleepscore { get; set; }
        public int dietscore { get; set; }
        public int sportscore { get; set; }
        public int totalscore { get; set; }

        public Record(string userId) 
        {
            RecordId = Guid.NewGuid().ToString();
            this.UserId = userId;
            this.dateTime = DateTime.Now.Date;
            this.sleepscore = 0;
            this.dietscore = 0;
            this.sportscore = 0;
            this.totalscore = 0;
        }

        public Record(string id,string userId, int sleepscore, int dietscore, int sportscore, int totlescore)
        {
            RecordId = id;
            this.UserId = userId;
            this.sleepscore = sleepscore;
            this.dietscore = dietscore;
            this.sportscore = sportscore;
            this.totalscore = totlescore;
        }

        public Record(string id,string userId, DateTime dateTime, int sleepscore, int dietscore, int sportscore, int totlescore)
        {
            RecordId = id;
            this.UserId = userId;
            this.dateTime = dateTime;
            this.sleepscore = sleepscore;
            this.dietscore = dietscore;
            this.sportscore = sportscore;
            this.totalscore = totlescore;
        }

    }
}
