using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.Models
{
    public class CourseCategoryViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục")]
        [StringLength(120, ErrorMessage = "Tên danh mục không quá 120 ký tự")]
        public string Name { get; set; }

        public Nullable<long> ParentId { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
    }
}