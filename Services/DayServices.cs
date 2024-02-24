using Microsoft.EntityFrameworkCore;
using SellersManager.DataBase;
using SellersManager.Models;

namespace SellersManager.Services
{
    public class DayServices
    {
        private readonly AplicationContext _context;
        public DayServices(AplicationContext context)
        {
            _context = context;
        }

        public List<Day> GetAll()
        {
            return _context.Days.ToList();
        }

        public List<Day> GetById(int studentId)
        {
            return _context.Days.Include(day => day.Lessons).Where(day => day.StudentId == studentId).ToList();
        }

        public void Add(Day day)
        {
            _context.Days.Add(day);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            _context.Days.Remove(_context.Days.Find(id));
            _context.SaveChanges();
        }
    }
}
