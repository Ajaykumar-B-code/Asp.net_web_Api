using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class collaborativeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaborativeId { get; set; }

        public string CollaborativeEmail { get; set; }

        [ForeignKey("User")]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual user User { get; set; }

        [ForeignKey("NotesUser")]
        public int NotesId { get; set; }
        
        [JsonIgnore]
        public virtual NotesEntity NotesUser { get; set; }
        
        public DateTime CollabratedAt { get; set; } 
        
        public DateTime UpdatedAt { get; set; }
        
        public bool IsTrash { get; set; }  
    }
}
