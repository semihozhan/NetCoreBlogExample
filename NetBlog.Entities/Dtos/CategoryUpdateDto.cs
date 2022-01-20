using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        [MaxLength(70, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        public string Description { get; set; }

        [DisplayName("Kategori Notu")]
        [MaxLength(100, ErrorMessage = "{0} Max {1} Karakter Olmalıdır")]
        [MinLength(2, ErrorMessage = "{0} Min {0} Karakter Olmalıdır")]
        public string Note { get; set; }

        [DisplayName("Kategori Aktif mi ?")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        public bool IsActive { get; set; }

        [DisplayName("Kategori Silindi mi ?")]
        [Required(ErrorMessage = "{0} Boş Olmamalıdır")]
        public bool IsDeleted { get; set; }
    }
}
