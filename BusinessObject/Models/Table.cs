using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Table
    {
        [Key]
        public Guid TableID { get; set; }
        public required string TableName { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Empty;
        public ICollection<TableSession>? TableSessions { get; set; }
    }
    public enum TableStatus
    {
        [Display(Name = "Empty")]
        Empty,

        [Display(Name = "Playing")]
        Playing,

        [Display(Name = "WaitingPayment")]
        WaitingPayment
    }
}
