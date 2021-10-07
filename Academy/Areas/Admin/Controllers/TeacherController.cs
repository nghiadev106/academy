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
    public class TeacherController : BaseController
    {
        // GET: Admin/Teacher
        private readonly ITeacherRepository _TeacherRepository;

        public TeacherController(ITeacherRepository TeacherRepository)
        {
            _TeacherRepository = TeacherRepository;
        }

        public ActionResult Index()
        {
            var listTeacher = _TeacherRepository.GetListTeacherAdmin();
            var listTeacherModel = Mapper.Map<List<Teacher>, List<TeacherViewModel>>(listTeacher);
            if (listTeacherModel.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            ViewBag.ListTeacher = listTeacherModel;
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeacherCreateRequest TeacherModel)
        {
            if (ModelState.IsValid)
            {
                if (TeacherModel.LogoFile == null || TeacherModel.LogoFile.ContentLength == 0)
                {
                    TempData["error"] = "Bạn chưa chọn ảnh";
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(TeacherModel.LogoFile.FileName);
                    string extention = Path.GetExtension(TeacherModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    TeacherModel.Avatar = "/UploadFiles/Teachers/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Teachers/"), fileName);
                    TeacherModel.LogoFile.SaveAs(fileName);
                }
                var newTeacher = new Teacher();
                newTeacher.AddTeacher(TeacherModel);
                newTeacher.CreateDate = DateTime.Now;
                _TeacherRepository.Add(newTeacher);
                TempData["success"] = "Thêm mới thông tin giáo viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Thêm mới thông tin giáo viên thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _TeacherRepository.GetTeacherDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Giáo viên không tồn tại";
                return RedirectToAction("Index");
            }
            var viewModel = Mapper.Map<Teacher, TeacherViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TeacherViewModel TeacherModel)
        {
            if (ModelState.IsValid)
            {
                var oldTeacher = _TeacherRepository.GetTeacherDetail(TeacherModel.Id);
                if (TeacherModel.LogoFile == null || TeacherModel.LogoFile.ContentLength == 0)
                {
                    TeacherModel.Avatar = oldTeacher.Avatar;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(TeacherModel.LogoFile.FileName);
                    string extention = Path.GetExtension(TeacherModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    TeacherModel.Avatar = "/UploadFiles/Teachers/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/Teachers/"), fileName);
                    TeacherModel.LogoFile.SaveAs(fileName);
                }
                oldTeacher.LastEditDate = DateTime.Now;
                oldTeacher.UpdateTeacher(TeacherModel);
                _TeacherRepository.Update(oldTeacher);
                TempData["success"] = "Cập nhật thông tin giáo viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var model = _TeacherRepository.GetTeacherDetail(TeacherModel.Id);
                var viewModel = Mapper.Map<Teacher, TeacherViewModel>(model);
                TempData["error"] = "Cập nhật thông tin giáo viên thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(long id)
        {
            var model = _TeacherRepository.GetTeacherDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Giáo viên không tồn tại";
                return RedirectToAction("Index");
            }
            _TeacherRepository.Delete(id);
            TempData["success"] = "Xóa thông tin giáo viên thành công";
            return RedirectToAction("Index");
        }
    }
}