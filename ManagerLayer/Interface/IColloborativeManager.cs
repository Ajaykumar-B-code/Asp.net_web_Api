using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IColloborativeManager
    {
        public collaborativeEntity Addcollab(int UserId, int noteId, string collabemail);

        public List<string> DisplayCollobratedUser(int UserId, int NoteId);

        public collaborativeEntity Remove(int noteId, int userId, string collabemail);
    }
}
