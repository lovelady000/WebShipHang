﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipShop.Model.Models
{
    [Table("WebInformations")]
    public class WebInformation
    {
        [Key]
        public int ID { set; get; }

        public string HotLine { set; get; }
        public string Skyper { set; get; }
        public string Facebook { set; get; }
        public string LinkAppIOS { set; get; }
        public string LinkAppAndroid { set; get; }
        public string LinkAppWindowPhone { set; get; }
        public string Longitude { set; get; }
        public string Latitude { set; get; }
    }
}