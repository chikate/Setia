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
public class FriendsController(BaseContext db) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("Request/{userId}")]
    public async Task<IActionResult> SendRequest(Guid userId)
    {
        if (userId == UserId) return BadRequest("Cannot friend yourself.");

        var existingLink = await db.Links.FirstOrDefaultAsync(l =>
            (l.SourceId == UserId && l.TargetId == userId && l.SourceType == "User" && l.TargetType == "User") ||
            (l.SourceId == userId && l.TargetId == UserId && l.SourceType == "User" && l.TargetType == "User"));

        if (existingLink != null) return BadRequest("Relationship already exists.");

        var link = new LinkModel
        {
            SourceType = "User",
            SourceId = UserId,
            TargetType = "User",
            TargetId = userId,
            Metadata = "Pending"
        };

        db.Links.Add(link);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Accept/{requestId}")]
    public async Task<IActionResult> AcceptRequest(Guid requestId)
    {
        var link = await db.Links.FindAsync(requestId);
        if (link == null) return NotFound();
        if (link.TargetId != UserId) return Forbid();
        if (link.Metadata != "Pending") return BadRequest("Request not pending.");

        link.Metadata = "Accepted";
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Reject/{requestId}")]
    public async Task<IActionResult> RejectRequest(Guid requestId)
    {
        var link = await db.Links.FindAsync(requestId);
        if (link == null) return NotFound();
        if (link.TargetId != UserId) return Forbid();

        db.Links.Remove(link);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetFriends()
    {
        var friendLinks = await db.Links
            .Where(l => (l.SourceId == UserId || l.TargetId == UserId) &&
                        l.SourceType == "User" && l.TargetType == "User" &&
                        l.Metadata == "Accepted")
            .ToListAsync();

        var friendIds = friendLinks.Select(l => l.SourceId == UserId ? l.TargetId : l.SourceId).ToList();
        var friends = await db.Users.Where(u => friendIds.Contains(u.Id)).ToListAsync();

        return Ok(friends);
    }

    [HttpGet("Requests")]
    public async Task<IActionResult> GetRequests()
    {
        var requests = await db.Links
            .Where(l => l.TargetId == UserId &&
                        l.SourceType == "User" && l.TargetType == "User" &&
                        l.Metadata == "Pending")
            .ToListAsync();

        var senderIds = requests.Select(r => r.SourceId).ToList();
        var senders = await db.Users.Where(u => senderIds.Contains(u.Id)).ToListAsync();

        return Ok(senders.Select(s => new { RequestId = requests.First(r => r.SourceId == s.Id).Id, Sender = s }));
    }
}
