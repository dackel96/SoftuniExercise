using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    /*ResourceId*/
    /*Name - (up to 50 characters, unicode)*/
    /*Url - (not unicode)*/
    /*ResourceType Създаваме enum клас в който изброяваме желаните опций*/
    /*CourseId към това пропърти което сочи към Курсовете и създава връзка добавяме наше Навиигационно пропърти от тип Course което ще помогне при LINQ заявките по-късно*/
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(2048)")]
        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
