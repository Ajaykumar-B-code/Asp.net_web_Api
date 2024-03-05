using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public  interface IColloborativeRepository
    {
        public collaborativeEntity Addcollab(int UserId, int noteId, string collabemail);

        public List<string> DisplayCollobratedUser(int UserId, int NoteId);

        public collaborativeEntity Remove(int noteId, int userId, string collabemail);

    }
}
