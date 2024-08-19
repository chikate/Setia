﻿using Main.Data.Models.Base;

namespace Main.Data.Models
{
    public class BaseModel
    {
        public DateTime ExecutionDate { get; set; } = DateTime.SpecifyKind(DateTime.Now!, DateTimeKind.Utc);
        public Guid? AuthorId { get; set; } = null;
        public UserModel? AuthorData { get; set; } = null;
        public List<string>? Tags { get; set; } = null;
    }
}