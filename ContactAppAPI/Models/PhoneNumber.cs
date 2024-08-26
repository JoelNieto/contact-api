using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactAppAPI.Models
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public required string Number { get; set; }
        public string? Type { get; set; }
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        [SwaggerIgnore]
        [IgnoreDataMember]
        public Contact? Contact { get; }
    }
}