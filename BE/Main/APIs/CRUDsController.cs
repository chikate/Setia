using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services.Interfaces;
using Main.Standards.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.APIs;

public class PostsController(ICRUD<PostModel> CRUD) : CRUDController<PostModel>(CRUD);
public class UsersController(ICRUD<UserModel> CRUD) : CRUDController<UserModel>(CRUD);
public class QuestionAnswersController : CRUDController<QuestionAnswerModel>
{
    #region Dependency Injection
    private readonly IAuth _auth;
    private readonly GovContext _context;

    public QuestionAnswersController(ICRUD<QuestionAnswerModel> CRUD,
        GovContext context,
        IAuth auth
    ) : base(CRUD)
    {
        _context = context;
        _auth = auth;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> GetQuestionAnswereDistribution([FromQuery] Guid questionId)
    {
        try
        {
            var responseCounts = await _context.QuestionAnswers
                .Where(q => q.QuestionId == questionId)
                .GroupBy(q => q.AuthorId)
                .Select(g => g.Count())
                .ToListAsync();

            return Ok(responseCounts);
        }
        catch (Exception ex) { return BadRequest(ex); }
    }
}