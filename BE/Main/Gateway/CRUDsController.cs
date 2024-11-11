using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Main.Standards.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.Gateway;

public class PostsController(ICRUDService<PostModel> CRUD, IAuthService auth) : CRUDController<PostModel>(CRUD, auth);
public class UsersController(ICRUDService<UserModel> CRUD, IAuthService auth) : CRUDController<UserModel>(CRUD, auth);
public class QuestionAnswersController : CRUDController<QuestionAnswerModel>
{
    #region Dependency Injection

    private readonly GovContext _govContext;

    public QuestionAnswersController
    (
        ICRUDService<QuestionAnswerModel> CRUD,
        IAuthService auth,
        GovContext govContext
    ) : base(CRUD, auth)
    {
        _govContext = govContext;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> GetQuestionAnswereDistribution([FromQuery] Guid questionId)
    {
        try
        {
            var responseCounts = await _govContext.QuestionAnswers
                .Where(q => q.QuestionId == questionId)
                .GroupBy(q => q.AuthorId)
                .Select(g => g.Count())
                .ToListAsync();

            return Ok(responseCounts);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}