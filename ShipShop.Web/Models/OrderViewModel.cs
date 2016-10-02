using System;

namespace ShipShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        public string SenderName { set; get; }

        public string SenderAddress { set; get; }

        public string SenderMobile { set; get; }

        public int SenderRegionID { set; get; }

        public virtual RegionViewModel SenderRegion { set; get; }

        public string ReceiverName { set; get; }

        public string ReceiverAddress { set; get; }

        public string ReceiverMobile { set; get; }

        public int ReceiverRegionID { set; get; }

        public virtual RegionViewModel ReceiverRegion { set; get; }

        public string PaymentMethod { set; get; }

        public float PayCOD { set; get; }

        public string Note { set; get; }

        public string Username { set; get; }

        public string UserID { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public bool Status { set; get; }

        public bool? SenderView { set; get; }

        public bool? ReceiverView { set; get; }
    }
}