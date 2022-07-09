using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    /*One student can have many CourseEnrollments*/
    /*One student can have many HomeworkSubmissions*/
    /*One course can have many StudentsEnrolled*/
    /*One course can have many Resources*/
    /*One course can have many HomeworkSubmissions*/
    public class StudentCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
