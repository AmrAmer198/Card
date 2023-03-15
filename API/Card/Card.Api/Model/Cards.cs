using System.ComponentModel.DataAnnotations;
using Card.Api.Database; 
namespace Card.Api.Model
{
    public class Cards
    {
        [Key]
        public Guid Id { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public int CVC { get; set; }

    }
}
