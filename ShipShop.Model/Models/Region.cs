using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("Regions")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int AreaID { set; get; }

        public string Name { set; get; }

        [ForeignKey("AreaID")]
        public virtual Areas Areas { set; get; }
    }
}