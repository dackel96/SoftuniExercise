using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models
{
    //⦁	Id – integer, Primary Key
    //⦁	FirstName – text with max length 20 (required) 
    //⦁	LastName – text with max length 20 (required) 
    //⦁	Age – integer(required)
    //⦁	NetWorth – decimal (required)
    //⦁	PerformerSongs – a collection of type SongPerformer
    public class Performer
    {
        public Performer()
        {
            this.PerformerSongs = new HashSet<SongPerformer>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public decimal NetWorth { get; set; }

        public ICollection<SongPerformer> PerformerSongs { get; set; }
    }
}
