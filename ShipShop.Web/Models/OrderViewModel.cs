using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        public string SenderName { set; get; }

        public string SenderAddress { set; get; }

        public string SenderMobile { set; get; }

        public int SenderRegionID { set; get; }

        public string ReceiverName { set; get; }

        public string ReceiverAddress { set; get; }

        public string ReceiverMobile { set; get; }

        public int ReceiverRegionID { set; get; }

        public string PaymentMethod { set; get; }


        public float PayCOD { set; get; }

        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public bool Status { set; get; }
    }
}