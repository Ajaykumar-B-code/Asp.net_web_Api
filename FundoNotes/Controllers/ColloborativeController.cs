using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FundoNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColloborativeController : ControllerBase
    {
        public IColloborativeManager manager;
        public ColloborativeController(IColloborativeManager manager)
        {
            this.manager = manager;
        }

        [Authorize]
        [HttpPost]
        [Route("addcolab")]
        public ActionResult Add(int noteId, string collabemail)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.Addcollab(userId, noteId, collabemail);
                if (response != null)
                {
                    return Ok(new ResModel<collaborativeEntity> { Success = true, Message = "Colloborated successfully", Data = response });
                }
                return BadRequest(new ResModel<collaborativeEntity> { Success = false, Message = "Colloboration failed", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<collaborativeEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize, HttpGet]
        [Route("GetColabEmail")]
        public ActionResult displayCollabEmail(int notesId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.DisplayCollobratedUser(userId, notesId);
                if (response != null)
                {
                    return Ok(new ResModel<List<string>> { Success = true ,Message="Successfully Displayed collab email",Data=response});
                }
                return BadRequest(new ResModel<List<string>> { Success=false, Message ="Display failed",Data = null});
            }
            catch (Exception e) { 
                return BadRequest(new ResModel<List<string>> { Success = false,Message="Display failed",Data = null});; }
        }

        [Authorize, HttpDelete]
        [Route("RemoveCollab")]
        public ActionResult deleteCollabEmail(int notesId,string collabemail)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.Remove(userId, notesId, collabemail);
                if (response != null) 
                {
                    return Ok(new ResModel<collaborativeEntity> { Success = true, Message = "deletion success", Data = response });
                }
                return BadRequest(new ResModel<collaborativeEntity> { Success = false, Message = "deletion fail", Data = null });
            }
            catch (Exception e) {
                return BadRequest(new ResModel<collaborativeEntity> { Success = false, Message = e.Message, Data = null });
                    }
        }

    }


}
