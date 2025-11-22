using System.ComponentModel.DataAnnotations;

namespace DataAccessObject.Models
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryID { get; set; }
        public Category? Category { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<TableProduct>? TableProducts { get; set; }
    }
}
