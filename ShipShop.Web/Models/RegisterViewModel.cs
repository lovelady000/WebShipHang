using System.ComponentModel.DataAnnotations;

namespace ShipShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại!")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu!")]
        [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 kí tự!")]
        public string Password { set; get; }

        public bool Vendee { set; get; }

        public string WebOrShopName { set; get; }

        public string Adress { set; get; }

        public string Region { set; get; }
    }
}