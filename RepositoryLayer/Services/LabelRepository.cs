using Microsoft.AspNetCore.Server.IIS.Core;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepository:ILabelRepository
    {
        private readonly demoContext context;
        public LabelRepository(demoContext context)
        {
            this.context = context;
        }

        public LabelEntity AddCreationUsingNotes(int UserId, int NoteId, string NoteName)
        {
            LabelEntity label = new LabelEntity();
            LabelEntity labelEntity = context.LabelTable.FirstOrDefault(x => ((x.Id == UserId) && (x.NotesId == NoteId) && (!string.IsNullOrEmpty(x.LabelName))));   
            if (labelEntity == null)
               {
                  label.Id = UserId;
                  label.NotesId = NoteId;
                  label.LabelName = NoteName;
                  context.LabelTable.Add(label);
                  context.SaveChanges();
                  return label;
              }
            
                return labelEntity;      
            
        }

        public List<LabelEntity> GetAllLabel(int userId, string labelname)
        {
            return context.LabelTable.Where(x => (x.Id == userId) && (x.LabelName == labelname)).ToList();
        }


        public List<LabelEntity> UpdateLabel(int UserId, string oldlabelName, string newLabelName)
        {

            List<LabelEntity> labelEntities = GetAllSameLabel(oldlabelName);

            if (labelEntities.Count() > 0)
            {
                foreach (LabelEntity onelabel in labelEntities)
                {
                    onelabel.LabelName = newLabelName;
                    context.SaveChanges();
                }
                return labelEntities;
            }
            throw new Exception("Label did not found with that name");
        }

        public List<LabelEntity> GetAllSameLabel(string labelName)
        {
            return context.LabelTable.Where(x => x.LabelName == labelName).ToList();
        }

       

    }
}
