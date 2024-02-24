using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellersManager.Models
{
    public abstract class Task
    {
        //[Key]
        public int Id {  get; set; }
        [MaxLength(200)]
        public string Description {  get; set; }
        public bool Done { get; set; }

        [ForeignKey("LessonId")]
        public int LessonId { get; set; }
        public Lesson Lesson {  get; set; }

        public Task() { }

        protected Task(string description, bool done, Lesson lesson)
        {
            Description = description;
            Lesson = lesson;
            Done = done;
            LessonId = lesson.Id;
        }

        protected Task(string description, bool done, int lessonId, Lesson lesson)
        {
            Description = description;
            Done = done;
            LessonId = lessonId;
            Lesson = lesson;
        }
    }
}
