using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellersManager.Models
{
    public class Day
    {
        public int Id {  get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey ("StudentId")]
        public int StudentId {  get; set; }
        public Student Student { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public Day() { }
        public Day(DateTime dateTime, Student student)
        {
            DateTime = dateTime;
            Student = student;
            StudentId = student.Id;
        }
        public Day(DateTime dateTime, Student student, int studentId)
        {
            DateTime = dateTime;
            Student = student;
            StudentId = studentId;
        }

        public void AddTask(Lesson lesson, Avaliation task)
        {
            lesson.AddTask(task);
        }
    }
}
