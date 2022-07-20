using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MusicHub.Data.Models
{
    //⦁	Id – integer, Primary Key
    //⦁	Name – text with max length 40 (required)
    //⦁	ReleaseDate – date(required)
    //⦁	Price – calculated property(the sum of all song prices in the album)
    //⦁	ProducerId – integer, foreign key
    //⦁	Producer – the album's producer
    //⦁	Songs – a collection of all Songs in the Album
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price
            => this.Songs.Sum(x => x.Price);

        public int? ProducerId { get; set; }
        public Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; }


    }
}
