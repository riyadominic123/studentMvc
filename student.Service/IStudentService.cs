using System;
using System.Collections.Generic;
using System.Text;
using student.Model;
namespace student.Service
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        void AddStudent(Student student);
        Student GetStudentById(int id);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
