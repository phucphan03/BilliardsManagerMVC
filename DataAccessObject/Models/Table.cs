using System.ComponentModel.DataAnnotations;

namespace DataAccessObject.Models
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
        [Display(Name = "Trống")]
        Empty,

        [Display(Name = "Đang chơi")]
        Playing,

        [Display(Name = "Chờ thanh toán")]
        WaitingPayment
    }
}
