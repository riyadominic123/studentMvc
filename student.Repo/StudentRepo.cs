using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using student.Data;
using student.Model;

namespace student.Repo
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext _appDbContext;
        public StudentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Student> GetAll()
        {
            return _appDbContext.Students.ToList();
        }
        public void Add(Student student)
        {
            _appDbContext.Students.Add(student);
            _appDbContext.SaveChanges();
        }
        public Student GetById(int id)

        {
            return _appDbContext.Students.Find(id);
        }
        public void Update(Student student)
        {
            _appDbContext.Students.Update(student);
            _appDbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var student = _appDbContext.Students.Find(id);
            if (student != null)
            {
                _appDbContext.Students.Remove(student);
                _appDbContext.SaveChanges();

            }
        }

    }
}
