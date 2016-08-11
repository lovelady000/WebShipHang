using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { set; get; }

        public int OrderID { set; get; }

        [MaxLength(256)]
        public string NameProduct { set; get; }

        public string UrlProductDetail { set; get; }

        public string Note { set; get; }

        [ForeignKey("OrderID")]
        public virtual Order Order { set; get; }
    }
}