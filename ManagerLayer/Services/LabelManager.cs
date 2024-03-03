using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class LabelManager:ILabelManager
    {
        private readonly ILabelRepository repository;

        public LabelManager(ILabelRepository repository)
        {
            this.repository= repository;
        }

        public LabelEntity AddCreationUsingNotes(int UserId, int NoteId, string NoteName)
        {
            return repository.AddCreationUsingNotes(UserId,NoteId, NoteName);
        }

        public List<LabelEntity> GetAllLabel(int userId, string labelname)
        {
            return repository.GetAllLabel(userId,labelname);
        }

        public List<LabelEntity> UpdateLabel(int UserId, string oldlabelName, string newLabelName)
        {
            return repository.UpdateLabel(UserId, oldlabelName, newLabelName);
        }

        public LabelEntity Remove(int userId, int NotesId, string labelName)
        {
            return repository.Remove(userId, NotesId, labelName);   
        }
    }
}
