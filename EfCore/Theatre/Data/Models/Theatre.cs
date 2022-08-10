using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.Data.Models
{
    public class Theatre
    {

        public Theatre()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } //text with length [4, 30] (required)

        public sbyte NumberOfHalls { get; set; } //sbyte between [1…10] (required)
        [Required]
        public string Director { get; set; } //text with length [4, 30] (required)

        public ICollection<Ticket> Tickets { get; set; }
    }
}
/*⦁	Id – integer, Primary Key
⦁	Name – text with length [4, 30] (required)
⦁	NumberOfHalls – sbyte between [1…10] (required)
⦁	Director – text with length [4, 30] (required)
⦁	Tickets - a collection of type Ticket
*/