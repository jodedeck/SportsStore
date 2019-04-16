using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int amountOfMoney { get; set; }
        
        public string merchantId { get; set; }
   
    }
}