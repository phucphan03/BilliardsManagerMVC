using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace BusinessObject.Models
{
    public class CueStick
    {
        [Key]
        public Guid CueStickID { get; set; }
        public required string Name { get; set; }
        public string? Brand { get; set; }
        public string? ImagePath { get; set; }
        public decimal PricePerTurn { get; set; }
    }
}
