using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace student.Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = "";
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int Age {  get; set; }
        public int ClassId {  get; set; }   
        public Class? Class { get; set; }
    }
}
