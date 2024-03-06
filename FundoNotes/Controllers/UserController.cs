using CommonLayer;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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
        private readonly ILogger<UserController> logger;
        private readonly IDistributedCache cache;

        public UserController(IUserManager userManager,IBus _bus,demoContext context, ILogger<UserController> logger, IDistributedCache cache)
        { 
            this.userManager = userManager;
            this._bus = _bus;
            this.context = context;
            this.logger = logger;
            this.cache = cache;

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
                    logger.LogInformation("Register success");
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
                    logger.LogInformation("login success");
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
            string check = userManager.ForgetPassword(Email);
           var checkmail = context.UserTable.FirstOrDefault(x => x.Email == Email);
            if (checkmail != null) {
                var token = userManager.GenerateToken(checkmail.Email, checkmail.Id);
                mail.SendMail(Email, check);
                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(check);
                if (check != null)
                {
                    return Ok(new ResModel<String> { Success = true, Message = "Mail sent", Data = check });
                }
                return BadRequest(new ResModel<String>{ Success = false, Message = "Mail sent failed", Data = null });
            }
            else
            {
                return BadRequest(new ResModel<String> { Success = false,Message = "not found",Data = null});
            }
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

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]

        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;
                if (userManager.UserResetPassword(Email, model))
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Password Changed", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Password not changed", Data = false });
                }
            }
            catch (Exception exe)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = exe.Message, Data = false });
            }
        }

        [HttpGet]
        [Route("RegisteredUsers")]
        public async Task<List<user>> GetAlluser(int userid)
        {
            string cachedKey = Convert.ToString(userid);

            byte[] cachedData  =  await cache.GetAsync(cachedKey);
            List<user> list = new List<user>();
            if(cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                list = JsonSerializer.Deserialize<List<user>>(cachedDataString);
            }
            else
            {
                list = context.UserTable.Where(x=>x.Id == userid).ToList();

                string cachedDataString = JsonSerializer.Serialize(cachedData);
                var date = Encoding.UTF8.GetBytes(cachedDataString);

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                await cache.SetAsync(cachedKey, date, options);
            }
            return list;
        }

        // review
        [Authorize]
        [HttpPost]
        [Route("Addusertask")]
        public ActionResult AddUserTask(RegisterModel model)
        {
            var response = userManager.UserRegister(model);
            if(response != null)
            {
                return Ok(new ResModel<user> { Success = true, Message = "User Registered done", Data = response });
            }
            return BadRequest(new ResModel<user> { Success = false,Message ="User Registration failed",Data= null});
        }
    }
}
