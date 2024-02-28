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
    }
}
