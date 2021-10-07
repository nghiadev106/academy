using Academy.Data;
using Academy.Models;
using Academy.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly INewsRepository _newsRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public HomeController(IStudentRepository studentRepository, INewsRepository newsRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _newsRepository = newsRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var listStudent = _studentRepository.GetListStudent().Where(x => x.Status == 3).ToList();
            var listStudentModel = Mapper.Map<List<Student>, List<StudentViewModel>>(listStudent);
            ViewBag.ListStudent = listStudentModel;
            if (listStudentModel.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            ViewBag.StudentCount = _studentRepository.GetListStudent().Where(x => x.Status == 1).Count();
            ViewBag.StudentFeedBackCount = _studentRepository.GetListStudent().Where(x => x.Status == 3).Count();
            ViewBag.TeacherCount = _teacherRepository.GetListTeacherAdmin().Where(x => x.Status == 1).Count();
            ViewBag.CourseCount = _courseRepository.GetListCourseHome().Where(x => x.Status == 1).Count();
            ViewBag.NewsCount = _newsRepository.GetListNews().Where(x => x.Status == 1).Count();
            return View();
        }
    }
}