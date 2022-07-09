using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    /*CourseId*/
    /*Name - (up to 80 characters, unicode)*/
    /*Description - (unicode, not required)*/
    /*StartDate*/
    /*EndDate*/
    /*Price*/

    public class Course
    {
        public Course()
        {
            this.HomeworkSubmissions = new HashSet<Homework>();
            this.Resources = new HashSet<Resource>();
            this.StudentsEnrolled = new HashSet<StudentCourse>();
        }
        public int CourseId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; } // !!!! Връзка 1 към много One course can have many HomeworkSubmissions
        public ICollection<Resource> Resources { get; set; } // !!! отново връзка 1 към много One course can have many Resources
        public ICollection<StudentCourse> StudentsEnrolled { get; set; } //  Връзка с Мапинг Таблица
    }
}
