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
    public class NotesRepository: INotesRepository
    {
        private readonly demoContext context;

        public NotesRepository(demoContext context)
        {
            this.context = context;
        }

        public NotesEntity NotesCeation(NotesCreatioinModel model,int id)
        {
            NotesEntity notesEntity = new NotesEntity();
            notesEntity.Title = model.Title;
            notesEntity.Description = model.Description;
            notesEntity.Colour = null;
            notesEntity.Image = null;
            notesEntity.Id = id;
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
    }
}
