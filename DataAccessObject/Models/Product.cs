using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid? ProductImageID { get; set; }
        public ICollection<TableProduct>? TableProducts { get; set; }

        [ForeignKey("ProductImageID")]
        public Image? ProductImage { get; set; }
    }
}