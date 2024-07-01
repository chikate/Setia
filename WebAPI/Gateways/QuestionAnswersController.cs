using Base.Gateways.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Contexts.Gov;
using Setia.Models.Gov;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    public class QuestionAnswersController : CRUDController<QuestionAnswerModel>
    {
        private readonly IAuth _auth;
        private readonly GovContext _context;

        public QuestionAnswersController(GovContext context, ICRUD<QuestionAnswerModel> CRUD, IAuth auth) : base(CRUD) { _context = context; _auth = auth; }

        [HttpGet]
        public async Task<IActionResult> GetQuestionAnswereDistribution([FromQuery] Guid questionId)
        {
            try
            {
                var responseCounts = await _context.QuestionAnswers
                    .Where(q => q.QuestionId == questionId)
                    .GroupBy(q => q.Author)
                    .Select(g => g.Count())
                    .ToListAsync();

                return Ok(responseCounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}