using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        //[Column(Order = 1)]
        public int OrderID { set; get; }

        public int Quantitty { set; get; }

        [ForeignKey("OrderID")]
        public virtual Order Order { set; get; }
    }
}