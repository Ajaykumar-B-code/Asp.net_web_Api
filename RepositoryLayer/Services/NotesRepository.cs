using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private readonly demoContext context;

        public NotesRepository(demoContext context)
        {
            this.context = context;
        }

        public NotesEntity NotesCeation(NotesCreatioinModel model, int id)
        {
            NotesEntity notesEntity = new NotesEntity();
            notesEntity.Title = model.Title;
            notesEntity.Description = model.Description;
            notesEntity.Colour = null;
            notesEntity.Image = null;
            notesEntity.Id = id;
            notesEntity.Reminder = DateTime.Now;
            notesEntity.UpdatedAt = DateTime.Now;
            notesEntity.CreatedAt = DateTime.Now;
            notesEntity.IsArchive = false;
            notesEntity.isPin = false;
            notesEntity.NotesUser = null;
            context.NotesTable.Add(notesEntity);
            context.SaveChanges();
            return notesEntity;
        }
        public List<NotesEntity> GetNotes(int id)
        {
            return context.NotesTable.Where(x => x.Id == id).ToList();
        }

       
        public NotesEntity UpdateNotes(int userId, NotesCreatioinModel model, int NotesId)
        {

            var notesEntity = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == NotesId)));

            if (notesEntity == null)
            {
                throw new Exception("Notes did not found");
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                notesEntity.Title = model.Title;
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                notesEntity.Description = model.Description;
            }
            //  context.NotesTable.Update(notesEntity);
            context.SaveChanges();
            return notesEntity;
        }

        //public NotesEntity UpdateNotes(NotesCreatioinModel model, int NotesId)
        //{
        //    var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == NotesId);
        //    if (notesEntity == null)
        //    {
        //        throw new Exception("Notes did not found");
        //    }
        //    notesEntity.Title = (!string.IsNullOrEmpty(model.Title)) ? model.Title : notesEntity.Title;
        //    notesEntity.Description=(!string.IsNullOrEmpty(model.Description)) ? model.Description : notesEntity.Description;
        //    context.SaveChanges();
        //    return notesEntity;
        //}

        public NotesEntity Istrash(int userId, int notesId)
        {
            var notesEntiy = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == notesId)));
            if (notesEntiy != null)
            {
                notesEntiy.isTrash = true;
                context.SaveChanges();
            }
            return notesEntiy;
        }

        public NotesEntity Delete(int userId, int NotesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == NotesId)));
            if (notesEntity != null)
            {
                context.NotesTable.Remove(notesEntity);
                context.SaveChanges();
            }
            return notesEntity;
            throw new Exception("notes did not found");
        }

        public NotesEntity Addcolor(string colour, int NotesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == NotesId);
            if (notesEntity != null)
            {
                notesEntity.Colour = colour;
                context.NotesTable.Update(notesEntity);
                context.SaveChanges();
            }
            return notesEntity;

        }





    }
}
