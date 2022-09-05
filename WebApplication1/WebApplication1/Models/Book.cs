using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Book
    {

        public int BookId { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        public  Author Author { get; set; }
        [ForeignKey("Author")]
        public int ? AuthorId { get; set; } 

        public  Customer Borrower { get; set; }
        [ForeignKey("Borrower")]
        public int ? BorrowerId { get; set; } /// Set this "FK ? nullable" to set it null when insert

    }
}
