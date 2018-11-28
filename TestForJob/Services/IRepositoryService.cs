using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForJob.Models;

namespace TestForJob.Services
{
    public interface IRepositoryService
    {
        IEnumerable<Models.Task> GetTaskList();
        void AddTask(Models.Task task);
        void UpdateTask(Models.Task task);
        void DeleteTask(int id);
    }
}
