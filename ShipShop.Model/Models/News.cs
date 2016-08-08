using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Model.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int ID { set; get; }

        public string Name { set; get; }

        public string Url { set; get; }

        public int Order { set; get; }

        public bool Status { set; get; }
    }
}
