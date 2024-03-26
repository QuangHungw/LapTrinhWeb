using Microsoft.AspNetCore.Mvc;

namespace SV20T1020042.Web.Controllers
{
    public class TestControler : Controller
    {
        
        public IActionResult Save(Models.Person model)
        {
        return Json(model);
        }
    }
}
