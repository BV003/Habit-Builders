namespace habitsBuilderBackEnd.DTO
{
    public class GramsDto
    {
        // userid是字符串类型
        public string UserId { get; set; }

        // gram1到gram6也是字符串类型，根据前端发送的数据类型定义
        public int Gram1 { get; set; }
        public int Gram2 { get; set; }
        public int Gram3 { get; set; }
        public int Gram4 { get; set; }
        public int Gram5 { get; set; }
        public int Gram6 { get; set; }
    }
}
