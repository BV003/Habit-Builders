namespace habitsBuilderBackEnd.DTO
{
    public class TimeRangeRequestDto
    {
        // 假设userid是整数类型
        public string UserId { get; set; }

        // 定义两个属性来接收startTime和endTime
        // 这里假设时间以小时和分钟的形式接收，并转换为整数
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
    }
}
