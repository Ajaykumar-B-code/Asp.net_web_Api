using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace FundoNotes.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    public class LabelController:ControllerBase
    {

        public ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }
        [Authorize]
        [HttpPost]
        [Route("addLabel")]
        public ActionResult AddLabelFromNotes(int noteID,string Labelname)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.AddCreationUsingNotes(userId, noteID, Labelname);
                if (response != null)
                {
                    return Ok(new ResModel<LabelEntity> { Success = true, Message = "Label Added", Data = response });
                }
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = " label for the note already exits", Data = null });
            }
            catch (Exception ex) {
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("display")]
        public ActionResult GetAllLabel(string labelName)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.GetAllLabel(userId,labelName);
                if (response != null)
                {
                    return Ok(new ResModel<List<LabelEntity>> { Success = true, Message = " Lable display successfull", Data = response });
                }
                return BadRequest(new ResModel<List<LabelEntity>> { Success = false, Message = " label display failed", Data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<List<LabelEntity>> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("updateLabel")]
        public ActionResult UpdateLabel(string oldLabelName,string NewLabelName)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = manager.UpdateLabel(userId, oldLabelName, NewLabelName);
                if (response != null)
                {
                    return Ok(new ResModel<List<LabelEntity>> { Success = true, Message = "Label names updated succesfully", Data = response });
                }
                return BadRequest(new ResModel<List<LabelEntity>> { Success = false, Message = "label names updation failed", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<List<LabelEntity>> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("RemoveLabel")]
        public ActionResult RemoveLabel(int noteId, string labelname)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserID").Value);
                var response = manager.Remove(userId, noteId, labelname);
                if (response != null)
                {
                    return Ok(new ResModel<LabelEntity> { Success = true, Message = "label deleted from note sucessfull", Data = response });
                }
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = "Label deletion failed", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
    }
}
