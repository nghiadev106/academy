using Academy.Controllers;
using Academy.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Academy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ITeacherRepository, TeacherRepository>();
            container.RegisterType<IEmployRepository, EmployRepository>();
            container.RegisterType<IConfigSystemRepository, ConfigSystemRepository>();
            container.RegisterType<ICourseCategoryRepository, CourseCategoryRepository>();
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<INewsRepository, NewsRepository>();
            container.RegisterType<INewCategoryRepository, NewCategoryRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}