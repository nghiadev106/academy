using System.Web.Mvc;

namespace Academy.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Login", controller = "Account", id = UrlParameter.Optional },
                 new[] { "Academy.Areas.Admin.Controllers" }
            );
        }
    }
}