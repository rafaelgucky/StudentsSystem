using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SellersManager.Models
{
    public class Lesson : IComparable
    {
        public int Id { get; set; }
        public string Name {  get; set; }

        [ForeignKey ("DayId")]
        public int DayId { get; set; }
        public Day Day { get; set; }
        //[NotMapped]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        //[NotMapped]
        public ICollection<Note> Notes { get; set; } = new List<Note>();

        public Lesson() { }

        public Lesson(string name, Day day)
        {
            Name = name;
            Day = day;
            DayId = day.Id;
        }

        public Lesson(string name, int dayId, Day day)
        {
            Name = name;
            DayId = dayId;
            Day = day;
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
        }

        public void AddNote(Note note)
        {
            Notes.Add(note);
        }
        public IEnumerable<Note> GetNotes(DateTime initial, DateTime final)
        {
            return Notes.Where(note => note.Date >= initial && note.Date <= final);
        }

		public int CompareTo(object obj)
		{
			if (obj is Lesson)
			{
				Lesson lesson = obj as Lesson;
                return Name.CompareTo(lesson.Name);
		
            }
            return 0;
		}

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Lesson)
            {
                Lesson lesson = obj as Lesson;
                return lesson.Name.Equals(Name);
            }
            return false;
        }
    }
}
