using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {

        [DisplayName("Mevcut Şifreniz")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(5, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Girmiş olduğunuz yeni şifre ve tekrarı uyuşmamaktadır")]
        public string RepeatPassword { get; set; }
    }
}
