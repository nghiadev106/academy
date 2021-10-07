using Academy.Data;
using Academy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy.Mappings
{
    public static class EntityExtensions
    {
        public static void AddTeacher(this Teacher teacher, TeacherCreateRequest teacherModel)
        {
            teacher.Id = teacherModel.Id;
            teacher.Name = teacherModel.Name;
            teacher.Phone = teacherModel.Phone;
            teacher.Email = teacherModel.Email;
            teacher.Address = teacherModel.Address;
            teacher.Gender = teacherModel.Gender;
            teacher.Status = teacherModel.Status;
            teacher.DOB = teacherModel.DOB;
            teacher.Avatar = teacherModel.Avatar;
            teacher.Description = teacherModel.Description;
        }
        public static void UpdateTeacher(this Teacher teacher, TeacherViewModel teacherModel)
        {
            teacher.Id = teacherModel.Id;
            teacher.Name = teacherModel.Name;
            teacher.Phone = teacherModel.Phone;
            teacher.Email = teacherModel.Email;
            teacher.Address = teacherModel.Address;
            teacher.Gender = teacherModel.Gender;
            teacher.Status = teacherModel.Status;
            teacher.DOB = teacherModel.DOB;
            teacher.Avatar = teacherModel.Avatar;
            teacher.Description = teacherModel.Description;
        }

        public static void UpdateStudent(this Student student, StudentViewModel studentModel)
        {
            student.Id = studentModel.Id;
            student.Name = studentModel.Name;
            student.Phone = studentModel.Phone;
            student.Email = studentModel.Email;
            student.Address = studentModel.Address;
            student.Gender = studentModel.Gender;
            student.Status = studentModel.Status;
            student.DOB = studentModel.DOB;
            student.Avatar = studentModel.Avatar;
            student.CourseId = studentModel.CourseId;
            student.Description = studentModel.Description;
        }

        public static void AddStudent(this Student student, StudentCreateRequest studentModel)
        {
            student.Id = studentModel.Id;
            student.Name = studentModel.Name;
            student.Phone = studentModel.Phone;
            student.Email = studentModel.Email;
            student.Address = studentModel.Address;
            student.Gender = studentModel.Gender;
            student.Status = studentModel.Status;
            student.DOB = studentModel.DOB;
            student.Avatar = studentModel.Avatar;
            student.CourseId = studentModel.CourseId;
            student.Description = studentModel.Description;
        }

        public static void AddFeedback(this Student student, FeedBackViewModel studentModel)
        {
            student.Name = studentModel.Name;
            student.Phone = studentModel.Phone;
            student.Email = studentModel.Email;
            student.Description = studentModel.Description;
        }

        public static void UpdateEmploy(this Employ employ, EmployViewModel employModel)
        {
            employ.Id = employModel.Id;
            employ.Name = employModel.Name;
            employ.Phone = employModel.Phone;
            employ.Email = employModel.Email;
            employ.Address = employModel.Address;
            employ.Gender = employModel.Gender;
            employ.Status = employModel.Status;
            employ.DOB = employModel.DOB;
        }

        public static void UpdateSetting(this ConfigSystem configSystem, ConfigSystemViewModel configSystemVm)
        {
            configSystem.Logo = configSystemVm.Logo;
            configSystem.Address = configSystemVm.Address;
            configSystem.Hotline1 = configSystemVm.Hotline1;
            configSystem.Hotline2 = configSystemVm.Hotline2;
            configSystem.IdNo = configSystemVm.IdNo;
            configSystem.Phone = configSystemVm.Phone;
            configSystem.TitleDefault = configSystemVm.TitleDefault;
            configSystem.Description = configSystemVm.Description;
            configSystem.Infomation = configSystemVm.Infomation;
        }

        public static void UpdateCourseCategory(this CourseCategory courseCategory, CourseCategoryViewModel courseCategoryVm)
        {
            courseCategory.Id = courseCategoryVm.Id;
            courseCategory.ParentId = courseCategoryVm.ParentId;
            courseCategory.Name = courseCategoryVm.Name;
            courseCategory.Description = courseCategoryVm.Description;
            courseCategory.Status = courseCategoryVm.Status;
            courseCategory.CreateBy = courseCategoryVm.CreateBy;
        }

        public static void UpdateCourse(this Course course, CourseViewModel courseVm)
        {
            course.Id = courseVm.Id;
            course.TeacherId = courseVm.TeacherId;
            course.CourseCategoryId = courseVm.CourseCategoryId;
            course.Name = courseVm.Name;
            course.Description = courseVm.Description;
            course.Note = courseVm.Note;
            course.CountLesson = courseVm.CountLesson;
            course.Price = courseVm.Price;
            course.StartDate = courseVm.StartDate;
            course.EndDate = courseVm.EndDate;
            course.Status = courseVm.Status;
            course.Image = courseVm.Image;
        }

        public static void UpdateNewCategory(this NewCategory NewCategory, NewCategoryViewModel NewCategoryVm)
        {
            NewCategory.Id = NewCategoryVm.Id;
            NewCategory.Name = NewCategoryVm.Name;
            NewCategory.Description = NewCategoryVm.Description;
            NewCategory.Status = NewCategoryVm.Status;
            NewCategory.CreateBy = NewCategoryVm.CreateBy;
        }


        public static void UpdateNews(this News news, NewsViewModel newsVm)
        {
            news.Id = newsVm.Id;
            news.Title = newsVm.Title;
            news.Description = newsVm.Description;
            news.Detail = newsVm.Detail;
            news.Description = newsVm.Description;
            news.NewCategoryId = newsVm.NewCategoryId;
            news.Image = newsVm.Image;
            news.Type = newsVm.Type;
            news.Status = newsVm.Status;
        }
    }
}