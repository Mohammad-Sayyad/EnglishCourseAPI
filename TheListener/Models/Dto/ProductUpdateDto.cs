using System.ComponentModel.DataAnnotations;

namespace TheListener.Models.Dto
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        //public CategoryDto? Category { get; set; }
    }
}
