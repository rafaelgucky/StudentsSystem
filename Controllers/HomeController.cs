using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellersManager.DataBase;
using SellersManager.Models;
using SellersManager.Models.ViewModels;
using System.Diagnostics;
using System.IO.Enumeration;
using System.IO;
using SellersManager.Services;
using NuGet.Packaging;

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
            ViewData["Id"] = student.Id;
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
            ViewData["Id"] = id;
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
            return RedirectToAction(nameof(Index), new { Id = student.Id });
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
            ViewData["Id"] = id;

            if(id == 0)
            {
                return NotFound();
            }
            Student student = _studentService.SetStudent(id).Result;

            foreach(Day day in student.Days)
            {
                HashSet<Lesson> lessons = new HashSet<Lesson>();
                foreach (Lesson lesson in day.Lessons)
                {
                    lessons.Add(lesson);
                }
                day.Lessons = lessons;
            }

            return View(student);
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

        [HttpGet]
        public IActionResult AddLesson(int id, int dayId)
        {
            return View(new LessonViewModel(dayId));
        }

        [HttpPost]
        public IActionResult AddLesson(int id, Lesson lesson)
        {
            
            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { Id = id });
        }

        public IActionResult DeleteLesson(int id, int dayId, int lessonId)
        {
            _context.Lessons.Remove(_context.Lessons.Where(lesson => lesson.Id == lessonId).SingleOrDefault());
            _context.SaveChanges();

            return RedirectToAction(nameof(DayEdit), new { Id = id, dayId = dayId });
        }

        public async Task<IActionResult> AddNote(int id)
        {
            @ViewData["Id"] = id;

            Student student = await _studentService.SetStudent(id);

            List<Lesson> lessons = new List<Lesson>();

            foreach (Day day in student.Days)
            {
                lessons.AddRange(day.Lessons);
            }

            return View(new NoteViewModel(lessons));
		}

        [HttpPost]
        public IActionResult AddNote(int id, Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
            return RedirectToAction(nameof(Notes), new { Id = id });
        }










        public IActionResult Privacy(int id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}