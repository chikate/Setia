using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Main.Standards.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.APIs;

public class PostsController(ICRUDService<PostModel> CRUD, IAuthService auth) : CRUDController<PostModel>(CRUD, auth);
public class UsersController(ICRUDService<UserModel> CRUD, IAuthService auth) : CRUDController<UserModel>(CRUD, auth);
public class QuestionAnswersController : CRUDController<QuestionAnswerModel>
{
    #region Dependency Injection

    private readonly GovContext _context;

    public QuestionAnswersController
    (
        GovContext context,
        ICRUDService<QuestionAnswerModel> CRUD,
        IAuthService auth
    ) : base(CRUD, auth)
    {
        _context = context;
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
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}