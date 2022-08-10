using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.Data.Models
{
    public class Cast
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } //text with length [4, 30] (required)

        public bool IsMainCharacter { get; set; }
        [Required]
        public string PhoneNumber { get; set; } //text in the following format: "+44-{2 numbers}-{3 numbers}-{4 numbers}". Valid phone numbers are: +44-53-468-3479, +44-91-842-6054, +44-59-742-3119 (required)

        public int PlayId { get; set; } 
        public Play Play { get; set; }
    }
}