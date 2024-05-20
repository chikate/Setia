﻿using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setia.Models.Base
{
    public class UserProfileModel : DefinitionStruct
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public string? Username { get; set; }
        public UserModel? UserData { get; set; }

        public string? Avatar { get; set; }
        public string? Signiture { get; set; }
        public string? Name { get; set; }
    }
}