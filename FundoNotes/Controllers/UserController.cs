using CommonLayer;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IBus _bus;
        private readonly demoContext context;

        public UserController(IUserManager userManager,IBus _bus,demoContext context)
        { 
            this.userManager = userManager;
            this._bus = _bus;
            this.context = context; 

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
                string response = userManager.Userlogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<String> { Success = true, Message = "login sucessfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<String> { Success = false, Message = "Login failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<user> { Success=false, Message = ex.Message, Data = null});
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string Email)
        {
            send mail = new send();
            var check = userManager.ForgetPassword(Email);
            var checkmail = context.UserTable.FirstOrDefault(x => x.Email == Email);
            var token = userManager.GenerateToken(checkmail.Email, checkmail.Id);
            mail.SendMail(Email, token);
            Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(check);
            return Ok(new ResModel<String> { Success = true, Message = "Mail sent", Data = token });
        }





        //[HttpPut]
        //[Route("{Email}")]
        //public ActionResult UserResetPassword(string Email, ResetPasswordModel model)
        //{
        //    try
        //    {
        //        var response = userManager.UserResetPassword(Email,model);
        //        if (response != null)
        //        {
        //            return Ok(new ResModel<user> { Success = true, Message = "changed sucessfull", Data = response });
        //        }
        //        else
        //        {
        //            return BadRequest(new ResModel<user> { Success = false, Message = "changed failed", Data = response });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ResModel<user> { Success = false, Message = ex.Message, Data = null });
        //    }
        //}


    }
}
