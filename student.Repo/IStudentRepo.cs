using System;
using System.Collections.Generic;
using System.Text;
using student.Model;

namespace student.Repo
{
    public interface IStudentRepo
    {
        List<Student> GetAll();
        void Add(Student student);
 
    }
}
