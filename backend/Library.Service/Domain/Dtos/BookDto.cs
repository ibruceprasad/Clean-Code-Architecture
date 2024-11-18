using System.ComponentModel.DataAnnotations;

namespace library.Services.Domain.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        
        [Required]
        [Range(0,5000)]
        public int Price { get; set; }
        
        public BookDto()
        {
            
        }

        public BookDto(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
