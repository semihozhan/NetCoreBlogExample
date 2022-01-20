using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage ="{0} Boş Olamaz")]
        [MaxLength(70,ErrorMessage ="{0} Max {1} Karakter Olmalı")]
        [MinLength(2,ErrorMessage ="{0} Min {0} Karakter Olmalı")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} Max {1} Karakter Olmalı")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalı")]
        public string Description { get; set; }

        [DisplayName("Kategori Notu")]
        [MaxLength(100, ErrorMessage = "{0} Max {1} Karakter Olmalı")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalı")]
        public string Note { get; set; }

        [DisplayName("Kategori Aktif mi ?")]
        [Required(ErrorMessage = "{0} Boş Olamaz")]
        public bool IsActive { get; set; }

    }
}
