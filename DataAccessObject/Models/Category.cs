namespace DataAccessObject.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public required string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
