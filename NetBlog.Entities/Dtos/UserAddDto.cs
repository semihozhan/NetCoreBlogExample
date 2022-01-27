using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public class UserAddDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        public string UserName { get; set; }

        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(100, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(10, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        public string Picture { get; set; }

    }
}
