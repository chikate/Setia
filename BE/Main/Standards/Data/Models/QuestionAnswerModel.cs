﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Standards.Data.Models
{
    public class QuestionAnswerModel : BaseModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required List<string> Answer { get; set; }

        [ForeignKey("Question")]
        public required Guid QuestionId { get; set; }
        public QuestionModel? QuestionData { get; set; }
    }
}