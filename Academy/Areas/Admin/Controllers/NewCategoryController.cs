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
    public class NewCategoryController : BaseController
    {
        // GET: Admin/NewCategory
        private readonly INewCategoryRepository _NewCategoryRepository;

        public NewCategoryController(INewCategoryRepository NewCategoryRepository)
        {
            _NewCategoryRepository = NewCategoryRepository;
        }

        public ActionResult Index()
        {
            var listNewCategory = _NewCategoryRepository.GetListNewCategory();
            var listlistNewCategoryModel = Mapper.Map<List<NewCategory>, List<NewCategoryViewModel>>(listNewCategory);
            ViewBag.ListNewCategory = listlistNewCategoryModel;
            if (listlistNewCategoryModel.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel NewCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var newNewCategory = new NewCategory();
                newNewCategory.UpdateNewCategory(NewCategoryModel);
                newNewCategory.Createdate = DateTime.Now;
                _NewCategoryRepository.Add(newNewCategory);
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
            var model = _NewCategoryRepository.GetNewCategoryDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Danh mục không tồn tại";
                return RedirectToAction("Index");
            }

            var viewModel = Mapper.Map<NewCategory, NewCategoryViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NewCategoryViewModel NewCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var oldNewCategory = _NewCategoryRepository.GetNewCategoryDetail(NewCategoryModel.Id);
                oldNewCategory.LastEditDate = DateTime.Now;
                oldNewCategory.UpdateNewCategory(NewCategoryModel);
                _NewCategoryRepository.Update(oldNewCategory);
                TempData["success"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var model = _NewCategoryRepository.GetNewCategoryDetail(NewCategoryModel.Id);
                var viewModel = Mapper.Map<NewCategory, NewCategoryViewModel>(model);
                TempData["error"] = "Cập nhật danh mục thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _NewCategoryRepository.GetNewCategoryDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Danh mục không tồn tại";
                return RedirectToAction("Index");
            }
            _NewCategoryRepository.Delete(id);
            TempData["success"] = "Xóa danh mục thành công";
            return RedirectToAction("Index");
        }
    }
}