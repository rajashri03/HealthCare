using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
        [HttpPost("Register")]
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
        [HttpPost("Login")]
        public IActionResult AdminLogin(LoginModel users)
        {
            try
            {
                var result = adminBl.Login(users);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to login" });
                }
            }
            catch (System.Exception e)
            {

                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [Authorize]
        [HttpPost("AddMedicine")]
        public IActionResult AddMedicine(MedicineModel medicine)
        {
            try
            {

                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userid").Value);
                //var email = User.Claims.First(e => e.Type == "email").Value;
                var result = adminBl.AddMedicine(medicine, UserID);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "medicine added successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add" });
                }
            }
            catch (System.Exception e)
            {

                return this.BadRequest(new { Success = false, message = e.Message });
            }
        }
        [HttpGet("AllMedicine")]
        public IEnumerable<GetAllMedicine> GetAllmedicine()
        {
            try
            {
                return adminBl.GetAllMedicines();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("MedicineByid")]
        public IEnumerable<GetAllMedicine> GetMedicineById()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userid").Value);
                return adminBl.GetAllMedicinesByid(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("AllDoctors")]
        public IEnumerable<GetDoctors> AllDoctors()
        {
            try
            {
                //int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userid").Value);
                return adminBl.GetAllDoctors();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Approve")]
        public IActionResult ApproveDisapprove(int userid,int approve)
        {
            try
            {
                var result = adminBl.ApproveORNot(userid,approve);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Success " });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
