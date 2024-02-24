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
            Student student = await _context.Students.FindAsync(id);
            student = _context.Students.Include(student => student.Days).FirstOrDefault(student => student.Id == id);
            

            //student.Days = _dayServices.GetById(id);

            student.Days.ForEach(day => day.Lessons =  _lessonServices.GetById(day.Id));

            student.Days.ForEach(day => day.Lessons.ForEach(lesson => lesson.Tasks = _lessonServices.GetTasks(lesson.Id)));

            student.Days.ForEach(day => day.Lessons.ForEach(lesson => lesson.Notes = _lessonServices.GetNotes(lesson.Id)));

            


            /*student.Days = await _context.Days.Where(day => day.Student.Equals(student)).ToListAsync();

            foreach(Day day in student.Days)
            {
                day.Lessons = await _context.Lessons.Where(lesson => lesson.Day.Id == day.Id).ToListAsync();
            }

            foreach (Day day in student.Days)
            {
                if(day.Lessons.Count < 1) { continue; }

                foreach(Lesson lesson in day.Lessons)
                {
                    lesson.Tasks.AddRange(_context.Avaliations.Where(avaliation => avaliation.Lesson.Id == lesson.Id));
                    lesson.Notes = _context.Notes.Where(note => note.Lesson.Id == lesson.Id).ToList();
                }
            }*/

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
            List<Day> days = _context.Days.Where(day => day.Student == student).ToList();
            _context.Days.RemoveRange(days);
            
        }
    }
}
