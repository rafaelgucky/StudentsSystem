using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellersManager.DataBase;
using SellersManager.Models;
using SellersManager.Models.ViewModels;
using System.Diagnostics;
using System.IO.Enumeration;
using System.IO;
using SellersManager.Services;

namespace SellersManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AplicationContext _context;
        private readonly StudentService _studentService;
        private readonly DayServices _dayServices;

        public HomeController(ILogger<HomeController> logger, AplicationContext context, StudentService studentService, DayServices dayServices)
        {
            _logger = logger;
            _context = context;
            _studentService = studentService;
            _dayServices = dayServices;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Student student)
        {
            if (_studentService.FindByNameAndPassword(student.Name, student.Password) == null)
            {
                return RedirectToAction(nameof (Login));
            }
            Student student1 = _studentService.FindByNameAndPassword(student.Name, student.Password);

            return RedirectToAction(nameof(Index), new { Id = student1.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if(id == 0)
            {
                return RedirectToAction(nameof(Login));
            }
            Student std = await _studentService.SetStudent(id);
            return View(std);
        }

        public IActionResult Register()
        {

            return View(new StudentViewModel());
        }
        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (!_studentService.IsValid(student))
            {
                return RedirectToAction(nameof(Register));
            }
            else if (_studentService.Exist(student))
            {
                return RedirectToAction(nameof(Index), new {Id = _studentService.FindByNameAndPassword(student.Name, student.Password).Id});
            }
            _studentService.Add(student);
            return RedirectToAction("Index", new { Id = student.Id });
        }

        [HttpGet]
        public IActionResult DayEdit(int? id, int dayId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = _studentService.SetStudent(id.Value).Result;
            return View(student.Days.Where(day => day.Id == dayId).SingleOrDefault());
        }

        [HttpGet]
        public IActionResult Delete(int id, int studentId)
        {
            if (id == 0)
            {
                return NotFound();
            }

            _dayServices.Delete(id);

            return RedirectToAction(nameof(Index), new {Id = studentId});

        }
        
        public async Task<IActionResult> Notes(int id)
        {
            List<Lesson> t = new List<Lesson>();
            Lesson lesson = new Lesson("Math", new Day());
            Lesson lesson1 = new Lesson("Por", new Day());
            Lesson lesson2 = new Lesson("Geo", new Day());
            lesson.AddNote(new Note(DateTime.Now, Models.Enums.NoteType.Avaliation, 9.0, new Lesson()));
            lesson.AddNote(new Note(DateTime.Now, Models.Enums.NoteType.Homework, 6.0, new Lesson()));
            lesson1.AddNote(new Note(DateTime.Now, Models.Enums.NoteType.Homework, 8.0, new Lesson()));
            lesson2.AddNote(new Note(DateTime.Now, Models.Enums.NoteType.Notebook, 7.0, new Lesson()));
            t.Add(lesson);
            t.Add(lesson1);
            t.Add(lesson2);
            if(id == 0)
            {
                id = 6;
                Student std = await _studentService.SetStudent(id);
                return View(std);
            }
            return View(new Student());
        }

        public IActionResult Calendar(string dayOfWeek)
        {
            return View(new Day(DateTime.Now, new Student()));
        }

        [HttpGet]
        public IActionResult AddDay(int id)
        {
            return View(new DayViewModel(id));
        }

        [HttpPost]
        public IActionResult AddDay(Day day)
        {
            day.Student = _studentService.Find(day.StudentId);

            _dayServices.Add(day);

            return RedirectToAction(nameof(Index), new { Id = day.StudentId });
        }

        public IActionResult DayDetails(Day day)
        {
            return View(day);
        }
















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}