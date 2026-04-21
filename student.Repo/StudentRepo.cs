using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using student.Data;
using student.Model;
using Microsoft.EntityFrameworkCore;

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
        public List<Student> GetAllWithClass()
        {
            return _appDbContext.Students
                .Include(s => s.Class)
                .ToList();
        }
        public List<Class> GetClasses()
        {
            return _appDbContext.Classes.ToList();
        }
        public UserClass GetUserClass(string userId)
        {
            return _appDbContext.UserClasses
                .FirstOrDefault(u => u.UserId == userId);
        }
        public void AddUserClass(UserClass userClass)
        {
            if (!_appDbContext.UserClasses.Any(u => u.UserId == userClass.UserId))
            {
                _appDbContext.UserClasses.Add(userClass);
                _appDbContext.SaveChanges();
            }
        }

    }
}
