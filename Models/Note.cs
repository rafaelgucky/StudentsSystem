using SellersManager.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellersManager.Models
{
    public class Note
    {
        public int Id { get; set; }
        public double Value {  get; set; }
        public DateTime Date { get; set; }
        public NoteType Type { get; set; }

        [ForeignKey("LessonId")]
        public int LessonId {  get; set; }
        public Lesson Lesson { get; set; }

        public Note() { }
        public Note(DateTime date, NoteType type, double value, Lesson lesson)
        {
            Date = date;
            Type = type;
            Value = value;
            Lesson = lesson;
            LessonId = lesson.Id;
        }
    }
}
