using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Theatre.Data.Models.Enums;

namespace Theatre.Data.Models
{
    public class Play
    {
        public Play()
        {
            this.Casts = new HashSet<Cast>();

            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; } //text with length [4, 50] (required)

        public TimeSpan Duration { get; set; } //TimeSpan in format {hours:minutes:seconds}, with a minimum length of 1 hour. (required)

        public float Rating { get; set; } //float in the range [0.00….10.00] (required)

        public Genre Genre { get; set; } //(required)
        [Required]
        public string Description { get; set; } //text with length up to 700 characters (required)
        [Required]
        public string Screenwriter { get; set; } //text with length [4, 30] (required)

        public ICollection<Cast> Casts { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}