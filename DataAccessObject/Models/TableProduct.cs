using System.ComponentModel.DataAnnotations;

namespace DataAccessObject.Models
{
    public class TableProduct
    {
        [Key]
        public Guid TableProductID { get; set; }
        public Guid TableSessionID { get; set; }
        public TableSession? TableSession { get; set; }
        public Guid ProductID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
