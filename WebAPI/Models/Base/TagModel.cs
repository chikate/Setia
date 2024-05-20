using Microsoft.EntityFrameworkCore;
using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Setia.Models.Base
{
    public class TagModel : DefinitionStruct
    {
        [Key]
        public LTree Tag { get; set; }
        public string? Comments { get; set; }
    }
}

[NotMapped]
public class LTreeConverter : JsonConverter<LTree>
{
    public override LTree Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) { if (reader.TokenType.Equals(typeof(LTree))) return reader.GetString(); throw new NotImplementedException(); }
    public override void Write(Utf8JsonWriter writer, LTree value, JsonSerializerOptions options) => writer.WriteStringValue(value);
}