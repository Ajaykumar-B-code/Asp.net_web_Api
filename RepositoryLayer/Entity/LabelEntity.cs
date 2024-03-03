using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("Users")]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual user Users { get; set; }

        [ForeignKey("noteusers")]
        public int NotesId { get; set; }

        [JsonIgnore]
        public virtual NotesEntity noteusers { get; set; }
    }
}
