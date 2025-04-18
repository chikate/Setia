using Main.Data.Models;
using Main.Modules.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.Features.CRUDs;

public class PostsController(ICRUDService<PostModel> CRUD, IAuthService auth) : CRUDController<PostModel>(CRUD, auth);
public class UsersController(ICRUDService<UserModel> CRUD, IAuthService auth) : CRUDController<UserModel>(CRUD, auth);
public class CommunityController(ICRUDService<CommunityModel> CRUD, IAuthService auth) : CRUDController<CommunityModel>(CRUD, auth);
public class QuestionAnswersController(ICRUDService<QuestionAnswerModel> CRUD, IAuthService auth) : CRUDController<QuestionAnswerModel>(CRUD, auth)
{
    [HttpGet]
    public async Task<IActionResult> GetQuestionAnswereDistribution([FromQuery] Guid questionId)
    {
        try
        {
            //var responseCounts = await _govContext.QuestionAnswers
            //    .Where(q => q.QuestionId == questionId)
            //    .GroupBy(q => q.AuthorId)
            //    .Select(g => g.Count())
            //    .ToListAsync();

            return Ok(/*responseCounts*/);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}