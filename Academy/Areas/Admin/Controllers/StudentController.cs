using Academy.Common;
using Academy.Data;
using Academy.Mappings;
using Academy.Models;
using Academy.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Areas.Admin.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Admin/Student
        private readonly IStudentRepository _StudentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _StudentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public ActionResult Index()
        {
            var listStudent= _StudentRepository.GetListStudent();
            var listStudentModel = Mapper.Map<List<Student>, List<StudentViewModel>>(listStudent);
            ViewBag.ListStudent = listStudentModel;
            if (listStudentModel.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            return View();
        }

        public ActionResult Detail(long id)
        {
            var model = _StudentRepository.GetStudentCourseDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Học sinh không tồn tại";
                return RedirectToAction("Index");
            }
            ViewBag.Detail = model;
            return View();
        }

        public ActionResult FeedBack()
        {
            var listStudent = _StudentRepository.GetListStudent().Where(x => x.Status == 3).ToList();
            var listStudentModel = Mapper.Map<List<Student>, List<StudentViewModel>>(listStudent);
            ViewBag.ListStudent = listStudentModel;
            if (listStudentModel.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            return View();
        }
        public ActionResult Create()
        {
            var courses = _courseRepository.GetListCourseHome();
            SelectList courseList = new SelectList(courses, "Id", "Name");
            ViewBag.courseList = courseList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentCreateRequest studentModel)
        {
            if (ModelState.IsValid)
            {
                if (studentModel.LogoFile == null || studentModel.LogoFile.ContentLength == 0)
                {
                    TempData["error"] = "Bạn chưa chọn ảnh";
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(studentModel.LogoFile.FileName);
                    string extention = Path.GetExtension(studentModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    studentModel.Avatar = "/UploadFiles/Students/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Students/"), fileName);
                    studentModel.LogoFile.SaveAs(fileName);
                }
                var newStudent = new Student();
                newStudent.AddStudent(studentModel);
                newStudent.CreateDate = DateTime.Now;
                _StudentRepository.Add(newStudent);
                TempData["success"] = "Thêm mới học sinh thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var courses = _courseRepository.GetListCourseHome();
                SelectList courseList = new SelectList(courses, "Id", "Name");
                ViewBag.courseList = courseList;

                TempData["error"] = "Thêm mới học sinh thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _StudentRepository.GetStudentDetail(id);            
            if (model == null)
            {
                TempData["warning"] = "Học sinh không tồn tại";
                return RedirectToAction("Index");
            }
            var courses = _courseRepository.GetListCourseHome();
            SelectList courseList = new SelectList(courses, "Id", "Name");
            ViewBag.courseList = courseList;
            var viewModel = Mapper.Map<Student, StudentViewModel>(model);           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel studentModel)
        {           
            if (ModelState.IsValid)
            {
                var oldStudent = _StudentRepository.GetStudentDetail(studentModel.Id);
                if (studentModel.LogoFile == null || studentModel.LogoFile.ContentLength == 0)
                {
                    studentModel.Avatar = oldStudent.Avatar;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(studentModel.LogoFile.FileName);
                    string extention = Path.GetExtension(studentModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    studentModel.Avatar = "/UploadFiles/Students/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Students/"), fileName);
                    studentModel.LogoFile.SaveAs(fileName);
                }
              
                oldStudent.LastEditDate = DateTime.Now;
                oldStudent.UpdateStudent(studentModel);
                _StudentRepository.Update(oldStudent);
                TempData["success"] = "Cập nhật thông tin học sinh thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var courses = _courseRepository.GetListCourseHome();
                SelectList courseList = new SelectList(courses, "Id", "Name");
                ViewBag.courseList = courseList;
                var model = _StudentRepository.GetStudentDetail(studentModel.Id);
                var viewModel = Mapper.Map<Student, StudentViewModel>(model);
                TempData["error"] = "Cập nhật thông tin học sinh thất bại";         
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _StudentRepository.GetStudentDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Học sinh không tồn tại";
                return RedirectToAction("Index");
            }
            _StudentRepository.Delete(id);
            TempData["success"] = "Xóa thông tin học sinh thành công";
            return RedirectToAction("Index");
        }
    }
}