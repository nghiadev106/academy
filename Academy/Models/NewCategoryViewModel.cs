using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.Models
{
    public class NewCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục")]
        [StringLength(120, ErrorMessage = "Tên danh mục không quá 120 ký tự")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
    }
}