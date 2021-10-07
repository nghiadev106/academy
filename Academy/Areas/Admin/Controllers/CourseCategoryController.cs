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

namespace Academy.Areas.Admin.Controllers
{
    public class CourseCategoryController : BaseController
    {
        // GET: Admin/CourseCategory
        private readonly ICourseCategoryRepository _courseCategoryRepository;

        public CourseCategoryController(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public ActionResult Index()
        {
            var listCourseCategory = _courseCategoryRepository.GetListCourseCategory();
            var listlistCourseCategoryModel = Mapper.Map<List<CourseCategory>, List<CourseCategoryViewModel>>(listCourseCategory);
            ViewBag.ListCourseCategory = listlistCourseCategoryModel;
            if (listlistCourseCategoryModel.Count() == 0)
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseCategoryViewModel CourseCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var newCourseCategory = new CourseCategory();
                newCourseCategory.UpdateCourseCategory(CourseCategoryModel);
                newCourseCategory.Createdate = DateTime.Now;
                _courseCategoryRepository.Add(newCourseCategory);
                TempData["success"] = "Thêm mới danh mục thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Thêm mới danh mục thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _courseCategoryRepository.GetCourseCategoryDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Danh mục không tồn tại";
                return RedirectToAction("Index");
            }
            var categories = _courseCategoryRepository.GetListCourseCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
          
            var viewModel = Mapper.Map<CourseCategory, CourseCategoryViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CourseCategoryViewModel CourseCategoryModel)
        {
            var categories = _courseCategoryRepository.GetListCourseCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            if (ModelState.IsValid)
            {
                var oldCourseCategory = _courseCategoryRepository.GetCourseCategoryDetail(CourseCategoryModel.Id);
                oldCourseCategory.LastEditDate = DateTime.Now;
                oldCourseCategory.UpdateCourseCategory(CourseCategoryModel);
                _courseCategoryRepository.Update(oldCourseCategory);
                TempData["success"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var model = _courseCategoryRepository.GetCourseCategoryDetail(CourseCategoryModel.Id);
                var viewModel = Mapper.Map<CourseCategory, CourseCategoryViewModel>(model);
                TempData["error"] = "Cập nhật danh mục thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _courseCategoryRepository.GetCourseCategoryDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Danh mục không tồn tại";
                return RedirectToAction("Index");
            }
            _courseCategoryRepository.Delete(id);
            TempData["success"] = "Xóa danh mục thành công";
            return RedirectToAction("Index");
        }
    }
}