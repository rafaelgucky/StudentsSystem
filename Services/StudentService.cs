using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using SellersManager.DataBase;
using SellersManager.Models;
using System.Threading;

namespace SellersManager.Services
{
    public class StudentService
    {
        private readonly AplicationContext _context;
        private readonly DayServices _dayServices;
        private readonly LessonServices _lessonServices;

        public StudentService(AplicationContext context, DayServices dayServices, LessonServices lessonServices)
        {
            _context = context;
            _dayServices = dayServices;
            _lessonServices = lessonServices;
        }

        public Student Find(int id)
        {
            return _context.Students.SingleOrDefault(student => student.Id == id);
        }

        public bool IsValid(Student student)
        {
            return student.Name != null && student.Password != null;
        }

        public bool Exist(Student student)
        {
            return _context.Students.Any(student2 => student2.Name == student.Name && student2.Password == student.Password);
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public Student FindByNameAndPassword(string name, string password)
        {
            return _context.Students.Where(student => student.Name == name && student.Password == password).SingleOrDefault();
        }

        public async Task<Student> SetStudent(int id)
        {
            Student student = await _context.Students.Include(student => student.Days).FirstOrDefaultAsync(student => student.Id == id);
            student.Days = _context.Days.Include(day => day.Lessons).Where(day => day.StudentId == id).ToList();
            student.Days.ToList().ForEach(day => day.Lessons = _context.Lessons.Include(lesson => lesson.Tasks).Include(x => x.Notes).Where(lesson => lesson.DayId == day.Id).ToList());

            return student;
        }

        public async System.Threading.Tasks.Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.Days.UpdateRange(student.Days);
            _context.Lessons.UpdateRange(student.Days.SelectMany(day => day.Lessons));
            _context.Notes.UpdateRange(student.Days.SelectMany(day => day.Lessons).SelectMany(lesson => lesson.Notes));
            try
            {
                _context.Avaliations.UpdateRange(student.Days.SelectMany(day => day.Lessons).SelectMany(lesson => lesson.Tasks) as IEnumerable<Avaliation>);
            }
            catch
            {
                _context.HomeWorks.UpdateRange(student.Days.SelectMany(day => day.Lessons).SelectMany(lesson => lesson.Tasks) as IEnumerable<HomeWork>);
            }

        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
            
        }
    }
}
