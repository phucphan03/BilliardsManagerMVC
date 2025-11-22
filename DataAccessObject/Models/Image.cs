using System.ComponentModel.DataAnnotations;

namespace DataAccessObject.Models
{
    public class Image
    {
        [Key]
        public Guid ImageID { get; set; }
        [Required]
        public required string ImageUrl { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? CueStickID { get; set; }
        public string PublicId { get; set; } = string.Empty;
    }
}
