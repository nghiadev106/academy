using Academy.Data;
using Academy.Mappings;
using Academy.Models;
using Academy.Repositories;
using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Academy.Areas.Admin.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Admin/Course
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CourseController(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _courseCategoryRepository = courseCategoryRepository;
            _teacherRepository = teacherRepository;
        }

        public ActionResult Index()
        {
            var listCourse = _courseRepository.GetListCourse();          
            ViewBag.ListCourse = listCourse;
            if (listCourse.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            return View();
        }
        public ActionResult Create()
        {
            var categories = _courseCategoryRepository.GetListCourseCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;

            var teachers = _teacherRepository.GetListTeacherCourse();
            SelectList teacherList = new SelectList(teachers, "Id", "Name");
            ViewBag.teacherList = teacherList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseViewModel CourseModel)
        {
            if (ModelState.IsValid)
            {
                if (CourseModel.LogoFile == null || CourseModel.LogoFile.ContentLength == 0)
                {
                    TempData["error"] = "Bạn chưa chọn ảnh";
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(CourseModel.LogoFile.FileName);
                    string extention = Path.GetExtension(CourseModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    CourseModel.Image = "/UploadFiles/Courses/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Courses/"), fileName);
                    CourseModel.LogoFile.SaveAs(fileName);
                }
                var newCourse = new Course();
                newCourse.UpdateCourse(CourseModel);
                newCourse.CreateDate = DateTime.Now;
                _courseRepository.Add(newCourse);
                TempData["success"] = "Thêm mới khóa học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var categories = _courseCategoryRepository.GetListCourseCategory();
                SelectList categoryList = new SelectList(categories, "Id", "Name");
                ViewBag.categoryList = categoryList;

                var teachers = _teacherRepository.GetListTeacherCourse();
                SelectList teacherList = new SelectList(teachers, "Id", "Name");
                ViewBag.teacherList = teacherList;
                TempData["error"] = "Thêm mới khóa học thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _courseRepository.GetCourseDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Khóa học không tồn tại";
                return RedirectToAction("Index");
            }
            var categories = _courseCategoryRepository.GetListCourseCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            var teachers = _teacherRepository.GetListTeacherCourse();
            SelectList teacherList = new SelectList(teachers, "Id", "Name");
            ViewBag.teacherList = teacherList;

            var viewModel = Mapper.Map<Course, CourseViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel CourseModel)
        {
            var categories = _courseCategoryRepository.GetListCourseCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            var teachers = _teacherRepository.GetListTeacherCourse();
            SelectList teacherList = new SelectList(teachers, "Id", "Name");
            ViewBag.teacherList = teacherList;
            if (ModelState.IsValid)
            {
                var oldCourse = _courseRepository.GetCourseDetail(CourseModel.Id);
                if (CourseModel.LogoFile == null || CourseModel.LogoFile.ContentLength == 0)
                {
                    CourseModel.Image = oldCourse.Image;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(CourseModel.LogoFile.FileName);
                    string extention = Path.GetExtension(CourseModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    CourseModel.Image = "/UploadFiles/Courses/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Courses/"), fileName);
                    CourseModel.LogoFile.SaveAs(fileName);
                }
                oldCourse.LastEditDate = DateTime.Now;
                oldCourse.UpdateCourse(CourseModel);
                _courseRepository.Update(oldCourse);
                TempData["success"] = "Cập nhật khóa học thành công";
                return RedirectToAction("Index");
            }
            else
            {

                var model = _courseRepository.GetCourseDetail(CourseModel.Id);
                var viewModel = Mapper.Map<Course, CourseViewModel>(model);
                TempData["error"] = "Cập nhật khóa học thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _courseRepository.GetCourseDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Khóa học không tồn tại";
                return RedirectToAction("Index");
            }
            _courseRepository.Delete(id);
            TempData["success"] = "Xóa khóa học thành công";
            return RedirectToAction("Index");
        }
    }
}