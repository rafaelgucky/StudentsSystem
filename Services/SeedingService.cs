using SellersManager.DataBase;
using SellersManager.Models;

namespace SellersManager.Services
{

	public class SeedingService
	{
		private readonly AplicationContext _context;

		public SeedingService() { }
		public SeedingService(AplicationContext context)
		{
			_context = context;
		}

		public void Seed()
		{
			Student student = new Student("Rafael", "1212");

			Day day = new Day(DateTime.Now, student);

			Lesson lesson1 = new Lesson("Math", day);
			Lesson lesson2 = new Lesson("Math", day);
			Lesson lesson3 = new Lesson("Math", day);
			Lesson lesson4 = new Lesson("Math", day);
			Lesson lesson5 = new Lesson("Math", day);

			Avaliation avaliation1 = new Avaliation("Exam today", false, lesson1);
			Avaliation avaliation2 = new Avaliation("Exam today", false, lesson1);
			Avaliation avaliation3 = new Avaliation("Exam today", false, lesson1);

			Note note1 = new Note(DateTime.Now, Models.Enums.NoteType.Avaliation, 8.5, lesson2);
			Note note2 = new Note(DateTime.Now, Models.Enums.NoteType.Avaliation, 7.5, lesson2);
			Note note3 = new Note(DateTime.Now, Models.Enums.NoteType.Avaliation, 6.5, lesson2);

			_context.Students.Add(student);
			_context.Days.Add(day);
			_context.Lessons.AddRange(lesson1, lesson2, lesson3, lesson4, lesson5);
			_context.Avaliations.AddRange(avaliation1, avaliation2, avaliation3);
			_context.Notes.AddRange(note1, note2, note3);

			_context.SaveChanges();
		}
	}
}
