using Academy.Data;
using Academy.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentViewModel>();
                cfg.CreateMap<Teacher, TeacherViewModel>();
                cfg.CreateMap<Employ, EmployViewModel>();
                cfg.CreateMap<ConfigSystem, ConfigSystemViewModel>();
                cfg.CreateMap<CourseCategory,CourseCategoryViewModel>();
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<News, NewsViewModel>();
                cfg.CreateMap<NewCategory, NewCategoryViewModel>();
            });
        }
    }
}