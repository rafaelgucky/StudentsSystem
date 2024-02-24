using SellersManager.DataBase;
using SellersManager.Models;

namespace SellersManager.Services
{
    public class LessonServices
    {
        private readonly AplicationContext _context;

        public LessonServices(AplicationContext context)
        {
            _context = context;
        }

        public List<Lesson> GetAll()
        {
            return _context.Lessons.ToList();
        }

        public List<Lesson> GetById(int dayId)
        {
            return _context.Lessons.Where(lesson => lesson.DayId == dayId).ToList();
        }

        public void Add(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
        }
        public List<Models.Task> GetTasks(int lessonId)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            tasks.AddRange(_context.Avaliations.Where(avaliation => avaliation.LessonId == lessonId).ToList());
            tasks.AddRange(_context.HomeWorks.Where(homework => homework.LessonId == lessonId).ToList());

            return tasks;
        }

        public List<Note> GetNotes(int lessonId)
        {
            return _context.Notes.Where(note => note.Lesson.Id == lessonId).ToList();
        }
        public void AddTask(Models.Task task)
        {
            if(task is Avaliation)
            {
                _context.Avaliations.Add(task as Avaliation);
            }
            else
            {
                _context.HomeWorks.Add(task as HomeWork);
            }
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
        }
    }
}
