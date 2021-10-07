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
    public class NewsController : BaseController
    {
        // GET: Admin/News
        private readonly INewsRepository _newsRepository;
        private readonly INewCategoryRepository _newsCategoryRepository;

        public NewsController(INewsRepository newsRepository, INewCategoryRepository newsCategoryRepository)
        {
            _newsRepository = newsRepository;
            _newsCategoryRepository = newsCategoryRepository;
        }

        public ActionResult Index()
        {
            var listNews = _newsRepository.GetListNews();
            //var listlistNewsModel = Mapper.Map<List<News>, List<NewsViewModel>>(listNews);
            ViewBag.ListNews = listNews;
            if (listNews.Count() == 0)
            {
                TempData["warning"] = "Không tìm thấy bản ghi nào";
            }
            return View();
        }
        public ActionResult Create()
        {
            var categories = _newsCategoryRepository.GetListNewCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsViewModel NewsModel)
        {
            if (ModelState.IsValid)
            {
                if (NewsModel.LogoFile == null || NewsModel.LogoFile.ContentLength == 0)
                {
                    TempData["error"] = "Bạn chưa chọn ảnh";
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(NewsModel.LogoFile.FileName);
                    string extention = Path.GetExtension(NewsModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    NewsModel.Image = "/UploadFiles/News/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/News/"), fileName);
                    NewsModel.LogoFile.SaveAs(fileName);
                }
                var newNews = new News();
                newNews.UpdateNews(NewsModel);
                newNews.CreateDate = DateTime.Now;
                _newsRepository.Add(newNews);
                TempData["success"] = "Thêm mới tin tức thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var categories = _newsCategoryRepository.GetListNewCategory();
                SelectList categoryList = new SelectList(categories, "Id", "Name");
                ViewBag.categoryList = categoryList;

                TempData["error"] = "Thêm mới tin tức thất bại";
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            var model = _newsRepository.GetNewsDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Khóa học không tồn tại";
                return RedirectToAction("Index");
            }
            var categories = _newsCategoryRepository.GetListNewCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;

            var viewModel = Mapper.Map<News, NewsViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NewsViewModel NewsModel)
        {
            var categories = _newsCategoryRepository.GetListNewCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;

            if (ModelState.IsValid)
            {
                var oldNews = _newsRepository.GetNewsDetail(NewsModel.Id);
                if (NewsModel.LogoFile == null || NewsModel.LogoFile.ContentLength == 0)
                {
                    NewsModel.Image = oldNews.Image;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(NewsModel.LogoFile.FileName);
                    string extention = Path.GetExtension(NewsModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    NewsModel.Image = "/UploadFiles/News/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/News/"), fileName);
                    NewsModel.LogoFile.SaveAs(fileName);
                }
               
                oldNews.LastEditdate = DateTime.Now;
                oldNews.UpdateNews(NewsModel);
                _newsRepository.Update(oldNews);
                TempData["success"] = "Cập nhật tin tức thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var model = _newsRepository.GetNewsDetail(NewsModel.Id);
                var viewModel = Mapper.Map<News, NewsViewModel>(model);
                TempData["error"] = "Cập nhật tin tức thất bại";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _newsRepository.GetNewsDetail(id);
            if (model == null)
            {
                TempData["warning"] = "Khóa học không tồn tại";
                return RedirectToAction("Index");
            }
            _newsRepository.Delete(id);
            TempData["success"] = "Xóa tin tức thành công";
            return RedirectToAction("Index");
        }
    }
}