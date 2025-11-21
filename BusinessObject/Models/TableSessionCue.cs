using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class TableSessionCue
    {
        [Key]
        public Guid TableSessionCueID { get; set; }
        public Guid TableSessionID { get; set; }
        public TableSession? TableSession { get; set; }
        public Guid CueStickID { get; set; }
        public CueStick? CueStick { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
