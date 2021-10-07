using Academy.Data;
using Academy.Mappings;
using Academy.Models;
using Academy.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly INewsRepository _newsRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _StudentRepository;
        private readonly IConfigSystemRepository _configSystemRepository;

        public HomeController(ICourseRepository courseRepository,
            INewsRepository newsRepository,
            ITeacherRepository TeacherRepository,
            IStudentRepository studentRepository,
            IConfigSystemRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _newsRepository = newsRepository;
            _teacherRepository = TeacherRepository;
            _StudentRepository = studentRepository;
            _configSystemRepository = teacherRepository;
        }
        public ActionResult Index()
        {
            var listCourse = _courseRepository.GetListCourseHome().Take(8).ToList();
            var listCourseModel = Mapper.Map<List<Course>, List<CourseViewModel>>(listCourse);
            ViewBag.ListCourse = listCourseModel;

            var listNews = _newsRepository.GetListNewsHome();
            var listNewsModel = Mapper.Map<List<News>, List<NewsViewModel>>(listNews);
            ViewBag.ListNews= listNewsModel;

            var listTeachers = _teacherRepository.GetListTeacherHome();
            ViewBag.ListTeachers = listTeachers;
            ViewBag.ActiveHome = "active";
            return View();
        }

        public ActionResult About()
        {
            var listTeachers = _teacherRepository.GetListTeacherHome();
            ViewBag.ListTeachers = listTeachers;
            ViewBag.ActiveAbout = "active";
            return View();
        }

        public ActionResult Admissions()
        {
            var listCourse = _courseRepository.GetListCourseHome().Take(10).ToList();
            var listCourseModel = Mapper.Map<List<Course>, List<CourseViewModel>>(listCourse);
            ViewBag.ListCourse = listCourseModel;
            ViewBag.ActiveAdmissions = "active";
            return View();
        }

        public ActionResult Course()
        {
            var listCourse = _courseRepository.GetListCourseHome().Take(4).ToList();
            var listCourseModel = Mapper.Map<List<Course>, List<CourseViewModel>>(listCourse);
            ViewBag.ListCourse = listCourseModel;
            ViewBag.ActiveCourse = "active";
            return View();
        }

        public ActionResult ListCourse()
        {
            var listCourse = _courseRepository.GetListCourseHome().ToList();
            var listCourseModel = Mapper.Map<List<Course>, List<CourseViewModel>>(listCourse);
            ViewBag.ListCourse = listCourseModel;
            ViewBag.ActiveListCourse = "active";
            return View();
        }

        public ActionResult CourseDetail(int id)
        {
            var course = _courseRepository.GetCourseDetail(id);
            var courseModel = Mapper.Map<Course, CourseViewModel>(course);
            ViewBag.courseModel = courseModel;
            return View();
        }

        public ActionResult News()
        {
            var listNews = _newsRepository.GetListNewsHome();
            var listNewsModel = Mapper.Map<List<News>, List<NewsViewModel>>(listNews);
            ViewBag.ListNews = listNewsModel;
            ViewBag.ActiveNews = "active";
            return View();
        }

        public ActionResult NewsDetail(int id)
        {
            var News = _newsRepository.GetNewsDetail(id);
            var NewsModel = Mapper.Map<News, NewsViewModel>(News);
            ViewBag.News = NewsModel;
            return View();
        }

        public ActionResult Contact()
        {
            var listConfigSystem = _configSystemRepository.GetConfigSystem(1);
            var listConfigSystemModel = Mapper.Map<ConfigSystem, ConfigSystemViewModel>(listConfigSystem);
            ViewBag.ActiveContact = "active";
            return View(listConfigSystemModel);          
        }

        [HttpPost]
        public ActionResult FeedBack(FeedBackViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var newStudent = new Student();
                newStudent.AddFeedback(model);
                newStudent.CreateDate = DateTime.Now;
                newStudent.Status = 3;
                _StudentRepository.Add(newStudent);
                ViewBag.Success  = "Đăng ký thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Đăng ký không thành công";
                return RedirectToAction("Index");
            }
        }
    }
}