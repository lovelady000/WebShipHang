using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("Areas")]
    public class Areas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaID { set; get; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(256)]
        public string Name { set; get; }

        public IEnumerable<Region> Region { set; get; }
    }
}