using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy.Models
{
    public class ConfigSystemViewModel
    {
        public long Id { get; set; }    
        public string Logo { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập địa chỉ")]
        [StringLength(120, ErrorMessage = "Tên nhân viên không quá 120 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập IdNo")]
        public string IdNo { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        [StringLength(11, ErrorMessage = "Tên nhân viên không quá 11 ký tự")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập Hotline1")]
        [StringLength(120, ErrorMessage = "Tên nhân viên không quá 120 ký tự")]
        public string Hotline1 { get; set; }

        [Required(ErrorMessage = "Bạn phải Hotline2")]
        [StringLength(120, ErrorMessage = "Tên nhân viên không quá 120 ký tự")]
        public string Hotline2 { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập mô tả")]
        [StringLength(500, ErrorMessage = "Tên nhân viên không quá 500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tiêu đề")]
        [StringLength(120, ErrorMessage = "Tên nhân viên không quá 120 ký tự")]
        public string TitleDefault { get; set; }

        [Required(ErrorMessage = "Mời nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        [StringLength(50, ErrorMessage = "Địa chỉ emai không quá 50 ký tự")]
        public string Infomation { get; set; }
        public HttpPostedFileWrapper LogoFile { get; set; }
    }
}