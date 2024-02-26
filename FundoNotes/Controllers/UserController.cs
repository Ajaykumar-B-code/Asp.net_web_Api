using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;


namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager )
        { 
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("reg")]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                var response = userManager.UserRegistration(model);
                if (response != null)
                {
                    return Ok(new ResModel<user> { Success = true, Message = "register successfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<user> { Success = false, Message = "Register failed", Data = response });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<user> { Success =false,Message= ex.Message,Data = null});
            }
            
           
        }

        [HttpPost]
        [Route("login")]
        public ActionResult login(login model)
        {
            try
            {
                var response = userManager.Userlogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<user> { Success = true, Message = "login sucessfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<user> { Success = false, Message = "Login failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<user> { Success=false, Message = ex.Message, Data = null});
            }
        }
        [HttpPut]
        [Route("{Email}")]
        public ActionResult UserResetPassword(string Email, ResetPasswordModel model)
        {
            try
            {
                var response = userManager.UserResetPassword(Email,model);
                if (response != null)
                {
                    return Ok(new ResModel<user> { Success = true, Message = "changed sucessfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<user> { Success = false, Message = "changed failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<user> { Success = false, Message = ex.Message, Data = null });
            }
        }
    }
}
