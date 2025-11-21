using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class TableSession
    {
        [Key]
        public Guid TableSessionID { get; set; }
        public Guid TableID { get; set; }
        public Table? Table { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double TotalHours { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<TableProduct>? TableProducts { get; set; }
        public ICollection<TableSessionCue>? TableSessionCues { get; set; }
    }

}
