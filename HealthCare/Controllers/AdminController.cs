using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminBL adminBl;
        public AdminController(IAdminBL adminBl)
        {
            this.adminBl = adminBl;
        }
        [HttpPost]
        public IActionResult Registration(UsersModel users)
        {
            try
            {
                var result = adminBl.Registration(users);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Registered successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add details" });
                }
            }
            catch (System.Exception e)
            {

                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }

    }
}
