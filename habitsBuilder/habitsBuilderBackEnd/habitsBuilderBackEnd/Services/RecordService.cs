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
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = starthour - endhour;
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

        public int Calculatediet(string userId, DateTime currentDateTime, int gram1, int gram2, int gram3, int gram4, int gram5,int gram6)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;

            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = gram6 - gram1;
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

        public int Calculatesport(string userId, DateTime currentDateTime, int time1, int time2, int time3)
        {
            // 仅截断当前时间到日期部分
            currentDateTime = currentDateTime.Date;
            // 从数据库中获取匹配的记录
            var record = dbContext.Records
                .FirstOrDefault(r => r.UserId == userId && r.dateTime.Date == currentDateTime.Date);
            if (record != null)
            {
                int ans = time3 - time1;
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
