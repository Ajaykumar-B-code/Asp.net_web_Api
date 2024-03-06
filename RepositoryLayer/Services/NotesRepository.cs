using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private readonly demoContext context;

        public NotesRepository(demoContext context)
        {
            this.context = context;
        }

        public NotesEntity NotesCeation(NotesCreatioinModel model, int id)
        {
            NotesEntity notesEntity = new NotesEntity();
            notesEntity.Title = model.Title;
            notesEntity.Description = model.Description;
            notesEntity.Colour = null;
            notesEntity.Image = null;
            notesEntity.Id = id;
            notesEntity.Reminder = DateTime.Now;
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

        public NotesEntity UpdateNotes(int userId, NotesCreatioinModel model, int NotesId)
        {

            var notesEntity = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == NotesId)));

            if (notesEntity == null)
            {
                throw new Exception("Notes did not found");
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                notesEntity.Title = model.Title;
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                notesEntity.Description = model.Description;
            }
            //  context.NotesTable.Update(notesEntity);
            context.SaveChanges();
            return notesEntity;
        }

        //public NotesEntity UpdateNotes(NotesCreatioinModel model, int NotesId)
        //{
        //    var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == NotesId);
        //    if (notesEntity == null)
        //    {
        //        throw new Exception("Notes did not found");
        //    }
        //    notesEntity.Title = (!string.IsNullOrEmpty(model.Title)) ? model.Title : notesEntity.Title;
        //    notesEntity.Description=(!string.IsNullOrEmpty(model.Description)) ? model.Description : notesEntity.Description;
        //    context.SaveChanges();
        //    return notesEntity;
        //}

        public NotesEntity Istrash(int userId, int notesId)
        {
            var notesEntiy = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == notesId)));
            if (notesEntiy != null)
            {
                notesEntiy.isTrash = true;
                context.SaveChanges();
            }
            return notesEntiy;
        }

        public NotesEntity Delete(int userId, int NotesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => ((x.Id == userId) && (x.NotesId == NotesId)));
            if (notesEntity != null)
            {
                context.NotesTable.Remove(notesEntity);
                context.SaveChanges();
            }
            return notesEntity;
            throw new Exception("notes did not found");
        }

        public NotesEntity Addcolor(string colour, int NotesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == NotesId);
            if (notesEntity != null)
            {
                notesEntity.Colour = colour;
                context.NotesTable.Update(notesEntity);
                context.SaveChanges();
            }
            return notesEntity;

        }

        public NotesEntity isArchieve(int userId, int notesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == notesId);
            if (notesEntity != null)
            {
                if (notesEntity.IsArchive)
                {
                    notesEntity.IsArchive = false;
                }
                else
                {
                    notesEntity.IsArchive = true;
                }
                context.SaveChanges();
                return notesEntity;
            }
            throw new Exception("notes did not found");
        }

        public NotesEntity isPin(int userId, int notesId)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == notesId);
            if (notesEntity != null)
            {
                if (notesEntity.isPin)
                {
                    notesEntity.isPin = false;
                }
                else
                {
                    notesEntity.isPin = true;
                }
                context.SaveChanges();
                return notesEntity;
            }
            throw new Exception("notes did not found");
        }

        public NotesEntity AddRemander(int notesId, DateTime dateTime)
        {
            var notesEntity = context.NotesTable.FirstOrDefault(x => x.NotesId == notesId);
            if (notesEntity != null)
            {
                notesEntity.Reminder = dateTime;
                context.Update(notesEntity);
                context.SaveChanges();
                return notesEntity;
            }
            throw new Exception("notes did not found");
        }

        public string UploadImage(string fpath,int notesId,int userId)
        {
           
                var notesEntityUserId = context.NotesTable.Where(x => x.Id==userId);
                if(notesEntityUserId != null)
                {
                    var notesEntityNoteId = notesEntityUserId.FirstOrDefault(x => x.NotesId == notesId);
                    if( notesEntityNoteId != null )
                    {
                        Account account = new Account("dnlfv7gzf", "228877752218316", "hZjydsNRUYKXUBFxGQ6l9wDW-Gw");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(fpath),
                            PublicId = notesEntityNoteId.Title
                        };

                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        notesEntityNoteId.UpdatedAt = DateTime.Now;
                        notesEntityNoteId.Image = uploadResult.Url.ToString();
                        context.SaveChanges();
                        return "Image uploaded Successfully";
                    }
                    return null;
                }
                return null;
         }

        // review
        public NotesEntity FindNote(int userId,string title,string description)
        {
            NotesEntity notesEntity = context.NotesTable.FirstOrDefault(x=>((x.Id==userId)&&(x.Title==title)&&(x.Description==description)));

            if( notesEntity != null)
            {
                return notesEntity;
            }
            throw new Exception("notes did not found");
        }

        public int CountofUser(int userId)
        {
            return context.NotesTable.Count(x => x.Id == userId);
        }
         }
     }



        //public AddLabelbyNotes(int userID,int noteId,string label)
        //{
        //    var notes= context.NotesTable.FirstOrDefault(x=>(x.Id==userID)&&(x.NotesId==noteId));
        //    var label = new LabelEntity();
        //    if( notes != null )
        //    {

        //    }
        //}
    



