namespace SellersManager.Models.ViewModels
{
	public class DayViewModel
	{
		public Day Day { get; set; }
        public int StudentId { get; set; }

		public DayViewModel() { }
		public DayViewModel(int studentId)
		{
			StudentId = studentId;
		}
    }
}
