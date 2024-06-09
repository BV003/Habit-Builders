using habitsBuilderBackEnd.DTO;
using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using habitsBuilderBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace habitsBuilderBackEnd.Controllers
{
    
    [Route("api/card")]
    public class CardController : ControllerBase
    {
        private readonly RecordDbContext _context;

        public CardController(RecordDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHabitCard([FromBody] HabitCardDTO habitCardDTO)
        {

            // 创建HabitCard对象
            var habitCard = new HabitCard
            {
                Description = habitCardDTO.Description,
                Category = habitCardDTO.Category,
                UserId = habitCardDTO.UserId,
                ChecklistItems = new List<ChecklistItem>()
            };

            // 添加 HabitCard 到数据库
            _context.HabitCards.Add(habitCard);
            await _context.SaveChangesAsync();

            return Ok(new { message = "创造卡片成功" });
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateChecklistItem([FromBody] ChecklistItemDTO checklistItemDTO)
        {
            // 查找对应的HabitCard
            var habitCard = await _context.HabitCards
                .Include(h => h.ChecklistItems)
                .FirstOrDefaultAsync(h => h.Id == checklistItemDTO.CardId && h.UserId == checklistItemDTO.UserId);

            if (habitCard == null)
            {
                return Ok(new { message = "卡片不存在" });
            }

            // 创建并添加ChecklistItem
            var checklistItem = new ChecklistItem
            {
                Item = checklistItemDTO.ChecklistItem,
                Status = false,
                Days = 0,
                CardId = checklistItemDTO.CardId
            };

            habitCard.ChecklistItems.Add(checklistItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "添加成功" });
        }
        [HttpDelete("items")]
        public async Task<IActionResult> DeleteChecklistItem([FromBody] ChecklistItemDTO checklistItemDTO)
        {
            var habitCard = await _context.HabitCards
                .Include(h => h.ChecklistItems)
                .FirstOrDefaultAsync(h => h.Id == checklistItemDTO.CardId && h.UserId == checklistItemDTO.UserId);

            if (habitCard == null)
            {
                return Ok(new { message = "卡片不存在" });
            }

            var checklistItem = habitCard.ChecklistItems
                .FirstOrDefault(i => i.Item == checklistItemDTO.ChecklistItem);

            if (checklistItem == null)
            {
                return Ok(new { message = "条目不存在" });
            }

            habitCard.ChecklistItems.Remove(checklistItem);
            _context.ChecklistItems.Remove(checklistItem);
            await _context.SaveChangesAsync();
            return Ok(new { message = "删除条目成功" });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteHabitCard([FromBody] DeleteHabitCardDTO deleteHabitCardDto)
        {
            var habitCard = await _context.HabitCards
                .Include(h => h.ChecklistItems)
                .FirstOrDefaultAsync(h => h.Id == deleteHabitCardDto.CardId && h.UserId == deleteHabitCardDto.UserId);

            if (habitCard == null)
            {
                return Ok(new { message = "卡片不存在" });
            }

            _context.ChecklistItems.RemoveRange(habitCard.ChecklistItems);
            _context.HabitCards.Remove(habitCard);
            await _context.SaveChangesAsync();
            return Ok(new { message = "删除卡片成功" });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllHabitCards(string userId)
        {
            var habitCards = await _context.HabitCards
                .Where(h => h.UserId == userId)
                .Include(h => h.ChecklistItems)
                .ToListAsync();

            var result = habitCards.Select(h => new
            {
                Id = h.Id,
                Description = h.Description,
                Category = h.Category,
                Checklist = h.ChecklistItems.Select(c => c.Item).ToList(),
                ChecklistStatus = h.ChecklistItems.Select(c => c.Status).ToList(),
                CheckDays = h.ChecklistItems.Select(c => c.Days).ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpPost("check")]
        public async Task<IActionResult> ClockInChecklistItem([FromBody] ChecklistItemDTO checklistItemDTO)
        {
            var habitCard = await _context.HabitCards
                .Include(h => h.ChecklistItems)
                .FirstOrDefaultAsync(h => h.Id == checklistItemDTO.CardId && h.UserId == checklistItemDTO.UserId);

            if (habitCard == null)
            {
                return Ok(new { message = "卡片不存在" });
            }

            var checklistItem = habitCard.ChecklistItems
                .FirstOrDefault(i => i.Item == checklistItemDTO.ChecklistItem);

            if (checklistItem == null)
            {
                return Ok(new { message = "条目不存在" });
            }
            if (checklistItem.Status == true)
            {
                checklistItem.Status = false;
            }
            else {
                checklistItem.Status = true;
            }
            
            await _context.SaveChangesAsync();
            return Ok(new { message = "打卡成功" });
        }

        

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAllChecklistItems()
        {
            var checklistItems = await _context.ChecklistItems
                    .Where(c => c.Status == true)
                    .ToListAsync();

            foreach (var item in checklistItems)
            {
                item.Days += 1;
                item.Status = false;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "更新成功" });
        }
        [HttpGet("checklist/{userId}")]
        public async Task<IActionResult> GetFalseStatusChecklistItems(string userId)
        {
            var checklistItems = await _context.ChecklistItems
                    .Where(c => c.Card.UserId == userId && c.Status == false)
                    .Select(c => new
                    {
                        c.Item,
                        c.CardId
                    })
                    .ToListAsync();

            return Ok(checklistItems);
        }

    }


    

    
}
