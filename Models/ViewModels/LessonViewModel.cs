namespace SellersManager.Models.ViewModels
{
    public class LessonViewModel
    {
        public int DayId {  get; set; }
        public Lesson Lesson { get; set; }

        public LessonViewModel(int dayId)
        {
            DayId = dayId;
        }
    }
}
