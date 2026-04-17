using System;
using System.Collections.Generic;
using System.Text;
using student.Model;

namespace student.Repo
{
    public interface IStudentRepo
    {
        List<Student> GetAll();
        Student GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
 
    }
}
