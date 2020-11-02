using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        // one-to-one relationship
        [Required]
        public virtual ContactInfo ContactInfo {get; set; }
    }
}