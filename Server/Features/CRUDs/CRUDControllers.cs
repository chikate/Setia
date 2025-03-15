using Main.Modules.Auth;
using Main.Modules.Gov;
using Main.Modules.Gov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.Features.CRUDs;

public class PostsController(ICRUDService<PostModel> CRUD, IAuthService auth) : CRUDController<PostModel>(CRUD, auth);
public class UsersController(ICRUDService<UserModel> CRUD, IAuthService auth) : CRUDController<UserModel>(CRUD, auth);
public class CommunityController(ICRUDService<CommunityModel> CRUD, IAuthService auth) : CRUDController<CommunityModel>(CRUD, auth);
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