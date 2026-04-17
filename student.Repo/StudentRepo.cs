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
    }
}
