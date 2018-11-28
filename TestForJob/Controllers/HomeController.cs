using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestForJob.Models;
using TestForJob.Models.ViewModels;
using TestForJob.Services;

namespace TestForJob.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryService _repositoryService;

        public HomeController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }
        public IActionResult Tasks(string searchString)
        {
            var tasksViewModel = new TasksViewModel();
            tasksViewModel.Tasks = _repositoryService.GetTaskList();
                        
            var taskViewModel = tasksViewModel.Tasks
                .Select( x =>
                    new TaskViewModel {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description });

            if (!String.IsNullOrEmpty(searchString))
            {
                taskViewModel = taskViewModel.Where(s => s.Name.Contains(searchString));
            }

            return View(taskViewModel);
        }
        
        [HttpGet]
        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(TaskViewModel taskViewModel)
        {
            try
            {
                _repositoryService.AddTask(new Models.Task { Name = taskViewModel.Name, Description = taskViewModel.Description });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Tasks));
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var tasks = _repositoryService.GetTaskList();
            var editTask = tasks.SingleOrDefault(task => task.Id == id);

            var editTaskViewModel = new TaskViewModel {  Id = editTask.Id, Name = editTask.Name, Description = editTask.Description };
            
            return View(editTaskViewModel);
        }

        [HttpPost]
        public IActionResult EditTask(int id, TaskViewModel taskViewModel)
        {            
            var task = new Models.Task { Id = id, Name = taskViewModel.Name, Description = taskViewModel.Description };

            try
            {
                _repositoryService.UpdateTask(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Tasks));
        }

        public IActionResult DetailsTask(int id)
        {
            var tasks = _repositoryService.GetTaskList();
            var taskDetails = tasks.SingleOrDefault(t => t.Id == id);

            var taskViewModel = new TaskViewModel
            {
                Id = taskDetails.Id,
                Name = taskDetails.Name,
                Description = taskDetails.Description
            };

            return View(taskViewModel);
        }

        public IActionResult DeleteTask(int id)
        {
            try
            {
                _repositoryService.DeleteTask(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Tasks));
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
