using System;
using System.Collections.Generic;
using System.Text;
using student.Repo;
using student.Model;


namespace student.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _repo;
        public StudentService(IStudentRepo repo)
        {
            _repo = repo;
        }
        public List<Student> GetStudents()
        {
            return _repo.GetAll();
        }
        public void AddStudent(Student student)
        {
            _repo.Add(student);
        }
        public Student GetStudentById(int id)
        {
            return _repo.GetById(id);
        }
        public void DeleteStudent(int id)
        {
            _repo.Delete(id);
        }
        public void UpdateStudent(Student student)
        {
            _repo.Update(student);
        }

    }
}
