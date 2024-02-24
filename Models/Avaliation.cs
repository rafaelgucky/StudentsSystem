using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellersManager.Models
{

    public class Avaliation : Task
    {

        public Avaliation() { }
        public Avaliation(string description, bool done, Lesson lesson) : base(description, done, lesson)
        {
        }

    }
}
