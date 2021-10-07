using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Academy.Models
{
    public class CourseViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên khóa học")]
        [StringLength(120, ErrorMessage = "Tên danh mục không quá 120 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn danh mục")]
        public Nullable<long> TeacherId { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn giáo viên")]
        public Nullable<long> CourseCategoryId { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mô tả")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập ghi chú")]
        [AllowHtml]
        public string Note { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số buổi học")]
        public Nullable<int> CountLesson { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập học phí")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày bắt đầu")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày kết thúc")]
        public Nullable<System.DateTime> EndDate { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public Nullable<System.DateTime> LastEditDate { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }

        public HttpPostedFileWrapper LogoFile { get; set; }
    }
}