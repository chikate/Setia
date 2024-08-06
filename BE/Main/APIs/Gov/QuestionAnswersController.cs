using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services.Base.Interfaces;
using Main.Standards.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.APIs.Gov
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
                    .GroupBy(q => q.AuthorId)
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