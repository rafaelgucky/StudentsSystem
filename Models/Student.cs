using System.ComponentModel.DataAnnotations;

namespace SellersManager.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "The name field is required")]
        [MaxLength (50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The password field is required")]
        [MaxLength (20)]
        public string Password { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();

        public Student() { }
        public Student(string name)
        {
            Name = name;
        }
        public Student(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Student)
            {
                Student student = obj as Student;
                return Id.Equals(student.Id);
            }
            return false;
        }
    }
}
