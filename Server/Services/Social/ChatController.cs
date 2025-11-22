using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Services.Social;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController(BaseContext db) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("History/{otherUserId}")]
    public async Task<IActionResult> GetHistory(Guid otherUserId)
    {
        var messages = await db.Messages
            .Where(m => (m.AuthorId == UserId && m.To == otherUserId) ||
                        (m.AuthorId == otherUserId && m.To == UserId))
            .OrderBy(m => m.ExecutionDate)
            .ToListAsync();

        return Ok(messages);
    }
    [HttpPost("Group")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupModel model)
    {
        model.Id = Guid.NewGuid();
        db.Groups.Add(model);
        
        // Add creator as member (and owner by virtue of being first)
        db.Links.Add(new LinkModel
        {
            SourceType = "User",
            SourceId = UserId,
            TargetType = "Group",
            TargetId = model.Id,
            ExecutionDate = DateTime.UtcNow
        });
        
        await db.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPost("Group/{groupId}/Member")]
    public async Task<IActionResult> AddMember(Guid groupId, [FromBody] Guid userId)
    {
        // Check if group exists
        if (!await db.Groups.AnyAsync(g => g.Id == groupId)) return NotFound("Group not found");

        // Check if already member
        if (await db.Links.AnyAsync(l => l.SourceType == "User" && l.SourceId == userId && l.TargetType == "Group" && l.TargetId == groupId))
            return BadRequest("User already in group");

        db.Links.Add(new LinkModel
        {
            SourceType = "User",
            SourceId = userId,
            TargetType = "Group",
            TargetId = groupId,
            ExecutionDate = DateTime.UtcNow
        });

        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("Group")]
    public async Task<IActionResult> GetGroups()
    {
        var groupIds = await db.Links
            .Where(l => l.SourceType == "User" && l.SourceId == UserId && l.TargetType == "Group")
            .Select(l => l.TargetId)
            .ToListAsync();

        var groups = await db.Groups.Where(g => groupIds.Contains(g.Id)).ToListAsync();
        return Ok(groups);
    }

    [HttpGet("Group/{groupId}/History")]
    public async Task<IActionResult> GetGroupHistory(Guid groupId)
    {
        // Check membership
        if (!await db.Links.AnyAsync(l => l.SourceType == "User" && l.SourceId == UserId && l.TargetType == "Group" && l.TargetId == groupId))
            return Forbid();

        var messages = await db.Messages
            .Where(m => m.GroupId == groupId)
            .OrderBy(m => m.ExecutionDate)
            .ToListAsync();

        return Ok(messages);
    }
}
