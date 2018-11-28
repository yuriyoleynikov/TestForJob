using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForJob.Data;
using TestForJob.Models;

namespace TestForJob.Services
{
    public class DatabaseRepositoryService : IRepositoryService
    {
        private readonly MyDbContext _context;

        public DatabaseRepositoryService(MyDbContext context)
        {
            _context = context;
        }
                
        public IEnumerable<Models.Task> GetTaskList()
        {
            var result = _context.Tasks
                .Select(x =>
                    new Models.Task
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();

            return result;
        }

        public void AddTask(Models.Task task)
        {
            if (task == null)
                throw new KeyNotFoundException("Task was not found.");

            _context.Tasks.Add(new Data.Task
            {
                Name = task.Name,
                Description = task.Description
            });

            _context.SaveChanges();
        }
                
        public void UpdateTask(Models.Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var taskUpdate = _context.Tasks.SingleOrDefault(t => t.Id == task.Id);
            
            if (taskUpdate == null)
                throw new KeyNotFoundException("Task was not found.");

            taskUpdate.Name = task.Name;
            taskUpdate.Description = task.Description;

            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.SingleOrDefault(t => t.Id == id);

            if (task == null)
                throw new KeyNotFoundException("Task was not found.");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }        
    }
}
