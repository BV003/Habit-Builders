using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace habitsBuilderBackEnd.Services
{
    public class RecordService
    {
        RecordDbContext dbContext;

        public RecordService(RecordDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddUser(User user)
        {
           
            // 将新用户添加到数据库
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return true;
            //添加成功
        }

        public int GetUserTotalScore(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取所有匹配的记录
            var records = dbContext.Records
                .Where(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date)
                .ToList();

            // 在内存中找到最新的记录
            var latestRecord = records.FirstOrDefault();

            if (latestRecord != null)
            {
                return latestRecord.totalscore;
            }
            else
            {
                return 0;
            }
        }

        public int GetUserSleepScore(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取所有匹配的记录
            var records = dbContext.Records
                .Where(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date)
                .OrderByDescending(r => r.dateTime)
                .ToList();

            // 在内存中找到最新的记录
            var latestRecord = records.FirstOrDefault();

            if (latestRecord != null)
            {
                return latestRecord.sleepscore;
            }
            else
            {
                return 0;
            }
        }

        public int GetUserDietScore(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取所有匹配的记录
            var records = dbContext.Records
                .Where(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date)
                .OrderByDescending(r => r.dateTime)
                .ToList();

            // 在内存中找到最新的记录
            var latestRecord = records.FirstOrDefault();

            if (latestRecord != null)
            {
                return latestRecord.dietscore;
            }
            else
            {
                return 0;
            }
        }

        public int GetUserSportScore(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取所有匹配的记录
            var records = dbContext.Records
                .Where(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date)
                .OrderByDescending(r => r.dateTime)
                .ToList();

            // 在内存中找到最新的记录
            var latestRecord = records.FirstOrDefault();

            if (latestRecord != null)
            {
                return latestRecord.sportscore;
            }
            else
            {
                return 0;
            }
        }

        public List<ScoreRecordDto> GetUserScores(string userId, DateTime currentDateTime)
        {
            // 截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 查询数据库，获取指定用户的分数记录
            var scoreRecords = dbContext.Records
                .Where(r => r.UserId == userId && r.dateTime.Date <= currentDateTime)
                .GroupBy(r => r.dateTime.Date) // 按日期分组
                .Select(group => new ScoreRecordDto
                {
                    Date = group.Key, // 分组键是日期
                    Score = group.Max(r => r.totalscore) // 选择当天的最高分数
                })
                .OrderByDescending(record => record.Date) // 按日期降序排列
                .ToList();

            return scoreRecords;
        }

        public Record GetUserRecord(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                // 如果找到了匹配的Record，直接返回
                return record;
            }
            else
            {
                // 如果没有找到匹配的Record，则创建一个新的Record
                record = new Record(userId);
                // 将新创建的Record添加到数据库上下文
                dbContext.Records.Add(record);
                // 保存更改到数据库
                dbContext.SaveChanges();
                // 返回新创建的Record
                return record;
            }
        }

        //计算total的值，并且去数据库中去修改
        public int Calculatetotal(string userId, DateTime currentDateTime)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                if(record.sleepscore!=0&&record.sleepscore!=0&&record.dietscore!=0)
                {
                    record.totalscore = (record.dietscore+record.sleepscore+record.sportscore) / 3;
                    dbContext.SaveChanges(); // 保存更改到数据库
                    return record.totalscore;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                // 如果没有找到匹配的Record，则创建一个新的Record
                record = new Record(userId);
                // 将新创建的Record添加到数据库上下文
                dbContext.Records.Add(record);
                // 保存更改到数据库
                dbContext.SaveChanges();
                // 返回新创建的Record
                return 0;
            }
        }

        public int Calculatesleep(string userId, DateTime currentDateTime,int starthour,int startmin,int endhour,int endmin)
        {
            Console.WriteLine("2done");
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = CalSleep(starthour,startmin,endhour,endmin);
                record.sleepscore = ans;
                dbContext.SaveChanges();
                return ans;
            }
            else
            {
                // 如果没有找到匹配的Record，则创建一个新的Record
                record = new Record(userId);
                // 将新创建的Record添加到数据库上下文
                dbContext.Records.Add(record);
                // 保存更改到数据库
                dbContext.SaveChanges();
                // 返回新创建的Record
                return 0;
            }
        }

        public int CalSleep(int startHour, int startMin, int endHour, int endMin)
        {
            Console.WriteLine("3done");
            // 计算总睡眠时长(单位:分钟)
            int totalSleepMin;
            if (startHour > endHour)
            {
                // 时间跨越午夜12点
                totalSleepMin = ((24 * 60 - (startHour * 60 + startMin)) + (endHour * 60 + endMin));
            }
            else
            {
                totalSleepMin = ((endHour * 60 + endMin) - (startHour * 60 + startMin));
            }

            // 将总睡眠时长转换为小时
            double totalSleepHour = totalSleepMin / 60.0;

            // 根据睡眠时长计算健康指数
            int sleepScore;
            if (totalSleepHour >= 7 && totalSleepHour <= 9)
            {
                // 7-9小时为最佳睡眠时长,健康指数设为100
                sleepScore = 100;
            }
            else if (totalSleepHour < 7)
            {
                // 小于7小时,健康指数线性递减
                sleepScore = (int)(100 * (totalSleepHour / 7));
            }
            else
            {
                // 大于9小时,健康指数线性递减
                sleepScore = (int)(100 * (9 / totalSleepHour));
            }

            // 确保健康指数在1-100之间
            return Math.Max(1, Math.Min(100, sleepScore));
        }

        public int Calculatediet(string userId, DateTime currentDateTime, int gram1, int gram2, int gram3, int gram4, int gram5,int gram6)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = CalDiet(gram1,gram2,gram3,gram4,gram5,gram6);
                record.dietscore = ans;
                dbContext.SaveChanges();
                return ans;
            }
            else
            {
                // 如果没有找到匹配的Record，则创建一个新的Record
                record = new Record(userId);
                // 将新创建的Record添加到数据库上下文
                dbContext.Records.Add(record);
                // 保存更改到数据库
                dbContext.SaveChanges();
                // 返回新创建的Record
                return 0;
            }
        }

        public int CalDiet(int gram1, int gram2, int gram3, int gram4, int gram5, int gram6)
        {
            // 根据营养学建议,计算出各项指标的得分
            int carbohydrateScore = CalculateCarbohydrateScore(gram1);
            int proteinScore = CalculateProteinScore(gram2);
            int vegetableScore = CalculateVegetableScore(gram3);
            int fruitScore = CalculateFruitScore(gram4);
            int snackScore = CalculateSnackScore(gram5);
            int waterScore = CalculateWaterScore(gram6);

            // 将各项得分加权求和,得到最终健康指数
            int totalScore = (int)(0.2 * carbohydrateScore +
                                  0.2 * proteinScore +
                                  0.2 * vegetableScore +
                                  0.15 * fruitScore +
                                  0.1 * snackScore +
                                  0.15 * waterScore);

            // 确保健康指数在1-100之间
            return Math.Max(1, Math.Min(100, totalScore));
        }

        private int CalculateCarbohydrateScore(int gram)
        {
            // 碳水化合物摄入量在200-300g之间为最佳,得分100
            if (gram >= 200 && gram <= 300)
                return 100;
            // 低于200g或高于300g,得分按比例递减
            return (int)(100 * ((double)gram / 250));
        }

        private int CalculateProteinScore(int gram)
        {
            // 蛋白质摄入量在50-100g之间为最佳,得分100
            if (gram >= 50 && gram <= 100)
                return 100;
            // 低于50g或高于100g,得分按比例递减
            return (int)(100 * ((double)gram / 75));
        }

        private int CalculateVegetableScore(int gram)
        {
            // 蔬菜摄入量在300-500g之间为最佳,得分100
            if (gram >= 300 && gram <= 500)
                return 100;
            // 低于300g或高于500g,得分按比例递减
            return (int)(100 * ((double)gram / 400));
        }

        private int CalculateFruitScore(int gram)
        {
            // 水果摄入量在200-400g之间为最佳,得分100
            if (gram >= 200 && gram <= 400)
                return 100;
            // 低于200g或高于400g,得分按比例递减
            return (int)(100 * ((double)gram / 300));
        }

        private int CalculateSnackScore(int gram)
        {
            // 零食摄入量在50g以下为最佳,得分100
            if (gram <= 50)
                return 100;
            // 超过50g,得分按比例递减
            return (int)(100 * (50.0 / gram));
        }

        private int CalculateWaterScore(int gram)
        {
            // 水摄入量在1500-2500ml之间为最佳,得分100
            if (gram >= 1500 && gram <= 2500)
                return 100;
            // 低于1500ml或高于2500ml,得分按比例递减
            return (int)(100 * ((double)gram / 2000));
        }
        public int Calculatesport(string userId, DateTime currentDateTime, int time1, int time2, int time3)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = CalSport(time1,time2,time3);
                record.sportscore = ans;
                dbContext.SaveChanges();
                return ans;
            }
            else
            {
                // 如果没有找到匹配的Record，则创建一个新的Record
                record = new Record(userId);
                // 将新创建的Record添加到数据库上下文
                dbContext.Records.Add(record);
                // 保存更改到数据库
                dbContext.SaveChanges();
                // 返回新创建的Record
                return 0;
            }
        }
        public int CalSport(int time1, int time2, int time3)
        {
            // 定义健康运动的推荐时间（单位：分钟）
            int recommendedHighIntensity = 150; // 推荐高强度运动时间（每周）
            int recommendedModerateIntensity = 300; // 推荐中等强度运动时间（每周）

            // 将每天的运动时间换算成每周的运动时间（假设平均分配）
            int weeklyHighIntensity = time1 * 7;
            int weeklyModerateIntensity = time2 * 7;
            int weeklyMildIntensity = time3 * 7;

            // 计算高强度和中等强度运动的得分
            int scoreHighIntensity = (weeklyHighIntensity >= recommendedHighIntensity) ? 50 : (int)(50 * (weeklyHighIntensity / (float)recommendedHighIntensity));
            int scoreModerateIntensity = (weeklyModerateIntensity >= recommendedModerateIntensity) ? 50 : (int)(50 * (weeklyModerateIntensity / (float)recommendedModerateIntensity));

            // 平缓运动的得分根据时间的长短来计算
            int scoreMildIntensity = (int)(weeklyMildIntensity / 60f * 10);

            // 总分，最高110分（高强度50分，中等强度50分，平缓运动10分/小时）
            int totalScore = scoreHighIntensity + scoreModerateIntensity + scoreMildIntensity;

            // 将总分转换为1到100的范围内
            int ans = (int)((totalScore / 110f) * 100);

            // 确保分数在1到100之间
            return Math.Max(1, Math.Min(ans, 100));
        }



        public void AddRecord(Record record)
        {
            dbContext.Entry(record).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public void RemoveRecord(string recordId)
        {
            var record = dbContext.Records
                .SingleOrDefault(o => o.RecordId == recordId);
            if (record == null) return;

            dbContext.Records.Remove(record);
            dbContext.SaveChanges();
        }



        public void UpdateRecord(Record newrecord)
        {
            RemoveRecord(newrecord.RecordId);
            AddRecord(newrecord);
        }
    }
}
