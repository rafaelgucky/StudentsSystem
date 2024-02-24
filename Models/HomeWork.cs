using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellersManager.Models
{
    public class HomeWork : Task
    {
        public HomeWork() { }
        public HomeWork(string description, bool done, Lesson lesson) : base(description, done, lesson)
        {
        }
    }
}
