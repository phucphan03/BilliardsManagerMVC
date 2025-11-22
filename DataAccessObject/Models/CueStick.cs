using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace DataAccessObject.Models
{
    public class CueStick
    {
        [Key]
        public Guid CueStickID { get; set; }
        public required string Name { get; set; }
        public string? Brand { get; set; }
        public Guid? CueStickImageID { get; set; }
        public decimal PricePerTurn { get; set; }
        [ForeignKey("CueStickImageID")]
        public Image? CueStickImage { get; set; }
    }
}
