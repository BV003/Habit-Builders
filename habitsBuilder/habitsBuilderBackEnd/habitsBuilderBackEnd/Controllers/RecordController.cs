using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace habitsBuilderBackEnd.Controllers
{
    
        [ApiController]
        [Route("api/accessment")]
        public class RecordController : ControllerBase
        {
            private readonly RecordService recordService;
            public RecordController(RecordService recordService)
            {
                this.recordService = recordService;
            }

        [HttpPost("test")]
        public ActionResult<string> test(string userid)
        {
            User user = new User { UserId =userid  };
            if (recordService.AddUser(user))
            {
                return Ok("修改成功");
            }
            else
            {
                return Unauthorized("修改失败");
            }
        }

        [HttpGet("gettoday")]
        public ActionResult<int> Gettoday(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return BadRequest("用户ID不能为空");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            // 假设recordService是一个服务类，用于访问数据库记录
            int score = recordService.GetUserTotalScore(userid,currentDateTime);
            if (score > 0)
            {
                // 如果找到了用户并且score是有效的，返回score
                return Ok(score);
            }
            else
            {
                // 如果没有找到用户或者score不是有效的，返回NotFound
                //return NotFound(new { message = "用户不存在或分数无效" });
                return Ok(0);
            }
        }

        [HttpPost("posttoday")]
        public ActionResult<int> Posttoday([FromBody] UserIdRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return BadRequest("用户ID不能为空");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            //Record record = recordService.GetUserRecord(userid, currentDateTime);
            return recordService.Calculatetotal(request.UserId, currentDateTime);
        }

        [HttpGet("getsleep")]
        public ActionResult<int> Getsleep(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return BadRequest("用户ID不能为空");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            // 假设recordService是一个服务类，用于访问数据库记录
            int score = recordService.GetUserSleepScore(userid, currentDateTime);
            if (score > 0)
            {
                // 如果找到了用户并且score是有效的，返回score
                return Ok(score);
            }
            else
            {
                // 如果没有找到用户或者score不是有效的，返回NotFound
                return Ok(0);
            }
        }

        [HttpPost("postsleep")]
        public ActionResult<int> PostTimeRange([FromBody] TimeRangeRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid request.");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            return Ok(recordService.Calculatesleep(requestDto.UserId, currentDateTime, requestDto.StartHour, requestDto.StartMinute, requestDto.EndHour, requestDto.EndMinute));
        }

        [HttpPost("postdiet")]
        public ActionResult<int> PostDiet([FromBody] GramsDto gramsDto)
        {
            if (gramsDto == null)
            {
                return BadRequest("Invalid request.");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            return Ok(recordService.Calculatediet(gramsDto.UserId, currentDateTime, gramsDto.Gram1, gramsDto.Gram2, gramsDto.Gram3, gramsDto.Gram4, gramsDto.Gram5, gramsDto.Gram6));
        }

        [HttpPost("postsport")]
        public ActionResult<int> PostSport([FromBody] SportDto sDto)
        {
            if (sDto == null)
            {
                return BadRequest("Invalid request.");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            return Ok(recordService.Calculatesport(sDto.UserId, currentDateTime, sDto.Time1, sDto.Time2, sDto.Time3));
        }

        [HttpGet("getdiet")]
        public ActionResult<int> Getdiet(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return BadRequest("用户ID不能为空");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            // 假设recordService是一个服务类，用于访问数据库记录
            int score = recordService.GetUserDietScore(userid, currentDateTime);
            if (score > 0)
            {
                // 如果找到了用户并且score是有效的，返回score
                return Ok(score);
            }
            else
            {
                // 如果没有找到用户或者score不是有效的，返回NotFound
                return Ok(0);
            }
        }

        [HttpGet("getsport")]
        public ActionResult<int> Getsport(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return BadRequest("用户ID不能为空");
            }
            DateTime currentDateTime = DateTime.UtcNow;
            // 假设recordService是一个服务类，用于访问数据库记录
            int score = recordService.GetUserSportScore(userid, currentDateTime);
            if (score > 0)
            {
                // 如果找到了用户并且score是有效的，返回score
                return Ok(score);
            }
            else
            {
                // 如果没有找到用户或者score不是有效的，返回NotFound
                return Ok(0);
            }
        }

        [HttpGet("gethistory")]
        public IActionResult GetUserScores(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "用户ID不能为空" });
            }

            // 获取当前UTC时间
            DateTime currentDateTime = DateTime.UtcNow;

            // 调用服务层方法获取分数记录
            List<ScoreRecordDto> scoreRecords = recordService.GetUserScores(userId, currentDateTime);

            if (scoreRecords.Any())
            {
                return Ok(scoreRecords);
            }
            else
            {
                return NotFound(new { message = "未找到用户分数记录" });
            }
        }

    }
    
}
