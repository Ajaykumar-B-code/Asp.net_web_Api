using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Interfaces;
using ManagerLayer.Interface;

namespace ManagerLayer.Services
{
    public class NotesManager: INotesManager
    {
        private readonly INotesRepository repository;

        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }
        public NotesEntity NotesCeation(NotesCreatioinModel model, int id)
        {
            return repository.NotesCeation(model, id);
        }
        public List<NotesEntity> GetNotes(int id)
        {
            return repository.GetNotes(id);
        }

        public NotesEntity UpdateNotes(int userId, NotesCreatioinModel model, int NotesId)
        {
            return repository.UpdateNotes(userId,model, NotesId);
        }

        public NotesEntity Istrash(int userId, int notesId)
        {
            return repository.Istrash(userId,notesId);
        }
        public NotesEntity Delete(int userId, int NotesId)
        {
            return repository.Delete(userId,NotesId);
        }

        public NotesEntity Addcolor(string colour, int NotesId)
        {
            return repository.Addcolor(colour,NotesId);
        }
        public NotesEntity isArchieve(int userId, int notesId)
        {
            return repository.isArchieve(userId, notesId);
        }

        public NotesEntity isPin(int userId, int notesId)
        {
            return repository.isPin(userId, notesId);
        }
        public NotesEntity AddRemander(int notesId, DateTime dateTime)
        {
            return repository.AddRemander(notesId, dateTime);
        }
        public string UploadImage(string fpath, int notesId, int userId)
        {
            return repository.UploadImage(fpath, notesId, userId);
        }

        // review
        public NotesEntity FindNote(int userId, string title, string description)
        {
            return repository.FindNote(userId,title,description);
        }
        public int CountofUser(int userId)
        {
            return repository.CountofUser(userId);
        }
    }

}
