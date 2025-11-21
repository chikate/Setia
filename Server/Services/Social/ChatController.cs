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
}
