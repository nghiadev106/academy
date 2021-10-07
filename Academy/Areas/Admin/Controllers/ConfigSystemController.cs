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
    public class ConfigSystemController : BaseController
    {
        private readonly IConfigSystemRepository _configSystemRepository;

        public ConfigSystemController(IConfigSystemRepository teacherRepository)
        {
            _configSystemRepository = teacherRepository;
        }

        public ActionResult Index()
        {
            var listConfigSystem = _configSystemRepository.GetConfigSystem(1);
            var listConfigSystemModel = Mapper.Map<ConfigSystem, ConfigSystemViewModel>(listConfigSystem);           
            return View(listConfigSystemModel);
        }

        [HttpPost]
        public ActionResult Index(ConfigSystemViewModel ConfigSystemVm)
        {
            var ConfigSystem = _configSystemRepository.GetConfigSystem(1);
            if (ModelState.IsValid)
            {
                if (ConfigSystemVm.LogoFile == null || ConfigSystemVm.LogoFile.ContentLength==0)
                {
                    ConfigSystemVm.Logo = ConfigSystem.Logo;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(ConfigSystemVm.LogoFile.FileName);
                    string extention = Path.GetExtension(ConfigSystemVm.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    ConfigSystemVm.Logo = "/UploadFiles/logo/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/logo/"), fileName);
                    ConfigSystemVm.LogoFile.SaveAs(fileName);
                }
              
                ConfigSystem.UpdateSetting(ConfigSystemVm);
                _configSystemRepository.Update(ConfigSystem);
                TempData["success"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            else
            {               
                var listConfigSystem = _configSystemRepository.GetConfigSystem(1);
                var listConfigSystemModel = Mapper.Map<ConfigSystem, ConfigSystemViewModel>(listConfigSystem);
                TempData["error"] = "Cập nhật thất bại";
                return View(listConfigSystemModel);
            }
            
        }       
    }
}