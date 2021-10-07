using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tiêu đề tin tức")]
        [StringLength(120, ErrorMessage = "tiêu đề tin tức không quá 120 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        public string Description { get; set; }

        [AllowHtml]
        public string Detail { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn danh mục")]
        public Nullable<int> NewCategoryId { get; set; }

        public string Image { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastEditdate { get; set; }
        public string CreateBy { get; set; }
        public string LastEditBy { get; set; }
        public Nullable<int> Type { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái ")]
        public Nullable<int> Status { get; set; }
        public HttpPostedFileWrapper LogoFile { get; set; }
    }
}