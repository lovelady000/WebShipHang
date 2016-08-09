﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("DonViTieuBieu")]
    public class DonViTieuBieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Name { set; get; }

        public string Url { set; get; }

        public string Image { set; get; }

        public int Order { set; get; }
    }
}