using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string SenderName { set; get; }

        [Required]
        [MaxLength(256)]
        public string SenderAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string SenderMobile { set; get; }

        [Required]
        [MaxLength(256)]
        public string ReceiverName { set; get; }

        [Required]
        [MaxLength(256)]
        public string ReceiverAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string ReceiverMobile { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
    }
}