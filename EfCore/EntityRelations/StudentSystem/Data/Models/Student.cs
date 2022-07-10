using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    /*StudentId съдаржанието Id индикира на EF core че това ще е неговия Primary Key в базата*/
    /*Name - (up to 100 characters, unicode) стринга по default е unicode */
    /*PhoneNumber - (exactly 10 characters, not unicode, not required) използваме атрибут [Column(TypeName = "....")] за да укажем точния тип данни който да запишем в базата*/
    /*RegisteredOn по default DateTime е not null и не е нужно да поставяме [Requared]*/
    /*Birthday - (not required) с поставянето на '?' казваме че пропъртито може да е null*/

    public class Student
    {
        public Student()
        {
            this.HomeworkSubmissions = new HashSet<Homework>();
            this.CourseEnrollments = new HashSet<StudentCourse>();
        }
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "char(10)")]
        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; } // !!! връзка 1 към много One student can have many HomeworkSubmissions
        public ICollection<StudentCourse> CourseEnrollments { get; set; } // Връзка с Мапинг Таблица
    }
}
