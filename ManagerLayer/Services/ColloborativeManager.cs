using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class ColloborativeManager:IColloborativeManager
    {
        private readonly IColloborativeRepository repository;
        public ColloborativeManager(IColloborativeRepository repository)
        {
            this.repository = repository;
        }

        public collaborativeEntity Addcollab(int UserId, int noteId, string collabemail)
        {
            return repository.Addcollab(UserId, noteId, collabemail);
        }

        public List<string> DisplayCollobratedUser(int UserId, int NoteId)
        {
            return repository.DisplayCollobratedUser(UserId, NoteId);
        }
        public collaborativeEntity Remove(int noteId, int userId, string collabemail)
        {
            return repository.Remove(noteId, userId, collabemail);
        }
    }

}
