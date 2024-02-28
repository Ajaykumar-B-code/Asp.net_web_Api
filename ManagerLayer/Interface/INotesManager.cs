using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface INotesManager
    {

        public NotesEntity NotesCeation(NotesCreatioinModel model, int id);

        public List<NotesEntity> GetNotes(int id);

    }
}
