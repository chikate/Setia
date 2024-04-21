using Microsoft.EntityFrameworkCore;
using Setia.Models.Base;
using Setia.Models.Structs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
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
public class LTreeDataConverter : JsonConverter<TagModel>
{
    public override TagModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException(); // Not needed for serialization

    public override void Write(Utf8JsonWriter writer, TagModel value, JsonSerializerOptions options)
    // Not perfect but does the trick, a workaround for incompatible NLevel expresion.
    {
        writer.WriteStartObject();

        foreach (PropertyInfo property in value.GetType().GetProperties())
        {
            //if (property.PropertyType == typeof(LTree))
            //{
            //    string path = value.Tag.ToString();
            //    IEnumerable<string> nodes = path.Split('.').ToList();

            //    writer.WritePropertyName("name");
            //    writer.WriteStringValue(nodes.Last());

            //    writer.WritePropertyName("nodes");
            //    writer.WriteStartArray();
            //    foreach (string node in nodes) writer.WriteStringValue(node);
            //    writer.WriteEndArray();
            //}
            writer.WritePropertyName(property.Name[0].ToString().ToLower() + property.Name.ToString().Substring(1));
            writer.WriteStringValue(property.GetValue(value)?.ToString());
        }

        writer.WriteEndObject();
    }
}