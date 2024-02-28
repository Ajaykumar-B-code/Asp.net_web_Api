using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepository
    {
        public NotesEntity NotesCeation(NotesCreatioinModel model, int id);

        public List<NotesEntity> GetNotes(int id);
    }
}
