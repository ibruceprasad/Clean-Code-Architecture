using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace libraries.domain
{
    public class Book : EntityBase
    {
        public string Name { get; set; } = default!;
        public int Price { get; set; }
    }
    
}
