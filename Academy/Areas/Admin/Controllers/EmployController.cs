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
    public class EmployController : BaseController
    {
        // GET: Admin/Employ
        private readonly IEmployRepository _EmployRepository;

        public EmployController(IEmployRepository employRepository)
        {
            _EmployRepository = employRepository;
        }

        public ActionResult Index()
        {
            var listEmploy = _EmployRepository.GetListEmployAdmin();
            var listEmployModel = Mapper.Map<List<Employ>, List<EmployViewModel>>(listEmploy);
            ViewBag.ListEmploy = listEmployModel;
            if (listEmployModel.Count() == 0)
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
        public ActionResult Create(EmployViewModel EmployModel)
        {
            if (ModelState.IsValid)
            {
                var newEmploy = new Employ();
                newEmploy.UpdateEmploy(EmployModel);
                newEmploy.CreateDate = DateTime.Now;
                _EmployRepository.Add(newEmploy);
                TempData["success"] = "Thêm mới thông tin nhân viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Thêm mới thông tin nhân viên thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _EmployRepository.GetEmployDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Nhân viên không tồn tại";
                return RedirectToAction("Index");
            }
            var viewModel = Mapper.Map<Employ, EmployViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployViewModel EmployModel)
        {
            if (ModelState.IsValid)
            {
                var oldEmploy = _EmployRepository.GetEmployDetail(EmployModel.Id);
                oldEmploy.LastEditDate = DateTime.Now;
                oldEmploy.UpdateEmploy(EmployModel);
                _EmployRepository.Update(oldEmploy);
                TempData["success"] = "Cập nhật thông tin nhân viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var model = _EmployRepository.GetEmployDetail(EmployModel.Id);
                var viewModel = Mapper.Map<Employ, EmployViewModel>(model);
                TempData["error"] = "Cập nhật thông tin nhân viên thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _EmployRepository.GetEmployDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Nhân viên không tồn tại";
                return RedirectToAction("Index");
            }
            _EmployRepository.Delete(id);
            TempData["success"] = "Xóa thông tin nhân viên thành công";
            return RedirectToAction("Index");
        }
    }
}