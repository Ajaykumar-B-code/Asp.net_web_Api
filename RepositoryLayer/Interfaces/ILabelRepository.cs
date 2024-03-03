using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepository
    {
        public LabelEntity AddCreationUsingNotes(int UserId, int NoteId, string NoteName);

        public List<LabelEntity> GetAllLabel(int userId, string labelname);

        public List<LabelEntity> UpdateLabel(int UserId, string oldlabelName, string newLabelName);

        public LabelEntity Remove(int userId, int NotesId, string labelName);
    }

}
