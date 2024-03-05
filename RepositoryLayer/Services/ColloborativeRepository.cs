using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ColloborativeRepository: IColloborativeRepository
    {
        private readonly demoContext context;
        public ColloborativeRepository(demoContext context)
        {
            this.context = context;
        }

        public collaborativeEntity Addcollab(int UserId,int noteId,string collabemail)
        {
            var mail = context.UserTable.FirstOrDefault(x=>x.Email == collabemail);
            var CollobarativeEntities = new collaborativeEntity();
            if (mail != null)
            {
                var checknoteExist = context.NotesTable.FirstOrDefault(x=>(x.Id==UserId)&&(x.NotesId==noteId));
                if (checknoteExist != null)
                {
                    CollobarativeEntities.Id = UserId;
                    CollobarativeEntities.NotesId = noteId;
                    CollobarativeEntities.CollaborativeEmail = collabemail;
                    CollobarativeEntities.CollabratedAt = DateTime.Now;
                    CollobarativeEntities.IsTrash = false;
                    context.Add(CollobarativeEntities);
                    context.SaveChanges();
                    return CollobarativeEntities;
                }
                throw new Exception("notes does not exist");
            }
            throw new Exception("mail does not exist");
        }

       


    }
}
