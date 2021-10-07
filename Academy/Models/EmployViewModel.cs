using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.Models
{
    public class EmployViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên nhân viên")]
        [StringLength(120, ErrorMessage = "Tên nhân viên không quá 120 ký tự")]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "Số điện thoại không quá 10 ký tự")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        [StringLength(120, ErrorMessage = "Tên địa chỉ không quá 120 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn giới tính")]
        public Nullable<int> Gender { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
    }
}