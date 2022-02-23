using Microsoft.AspNetCore.Mvc;
using TbcWeb.DataModels;

namespace TbcWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected AppDBContext db => (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
    }
}
