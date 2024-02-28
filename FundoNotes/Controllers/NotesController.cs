
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interface;
using ManagerLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace FundoNotes.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NotesController: ControllerBase
    {
        private INotesManager manager;

        public NotesController(INotesManager manager)
        {
            this.manager = manager;
        }
        [Authorize]
        [HttpPost]
        [Route("add")]

        public ActionResult Creation(NotesCreatioinModel model)
        {
            int id = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = manager.NotesCeation(model,id);
            if (response != null)
            {
                return Ok(new ResModel<NotesEntity> { Success = true, Message = "created successfull", Data = response });
            }
            return BadRequest(new ResModel<NotesEntity> { Success = false,Message="creation failed",Data= response });
        }

        [Authorize]
        [HttpGet]
        [Route("all")]
        public ActionResult All()
        {
            int id = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = manager.GetNotes(id);
            if (response != null)
            {
                return Ok(new ResModel<List<NotesEntity>> { Success = true, Message = "Notes displayed Sucessfully", Data = response });
            }
            return BadRequest(new ResModel<List<NotesEntity>> { Success = false, Message = "Notes displayed failed", Data = response });
        }

    }
}
