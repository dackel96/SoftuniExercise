using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    /*HomeworkId*/
    /*Content - (string, linking to a file, not unicode) контента е varchar(255) ще бъде линк към файл в Операционната система а линковете в нея са до 255 символа за (Windows)*/
    /*ContentType === enum*/
    /*SubmissionTime*/
    /*StudentId*/
    /*CourseId*/
    public class Homework
    {
        public int HomeworkId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
