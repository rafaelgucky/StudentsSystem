using NuGet.Packaging;
using SellersManager.Models.Enums;

namespace SellersManager.Models.ViewModels
{
    public class NoteViewModel
    {
        public Note Note { get; set; }
        public HashSet<Lesson> Lessons {  get; set; }

        public NoteViewModel(List<Lesson> lessons)
        {
            HashSet<Lesson> hashLessons = new HashSet<Lesson>();
            hashLessons.AddRange(lessons);
            Lessons = hashLessons;
        }
    }
}
