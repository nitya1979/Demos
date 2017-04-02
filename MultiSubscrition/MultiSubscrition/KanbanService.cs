using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MultiSubscrition
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class KanbanService : IKanbanService
    {
        private static ProjectDetail[] Projects;
        private static TaskDetail[] Tasks;
        private static List<IKanbanServiceCallback> subscription = new List<IKanbanServiceCallback>();

        static KanbanService()
        {
            Projects = new ProjectDetail[2];
            Projects[0] = new ProjectDetail { ProjectID = 1, Name = "Project First", ReleaseDate = DateTime.Today.AddDays(40) };
            Projects[1] = new ProjectDetail { ProjectID = 2, Name = "Project Second", ReleaseDate = DateTime.Today.AddDays(30) };

            Tasks = new TaskDetail[10];

            Tasks[0] = new TaskDetail { TaskId = 1, Summary = "Task First for Project One", ProjectID = 1, Priority = Priority.Critical, Description = "Description for Task First", DueDate = DateTime.Now.AddDays(3) };
            Tasks[1] = new TaskDetail { TaskId = 2, Summary = "Task Second for Project One", ProjectID = 1, Priority = Priority.High, Description = "Description for Task Second", DueDate = DateTime.Now.AddDays(5) };
            Tasks[2] = new TaskDetail { TaskId = 3, Summary = "Task Third for Project One", ProjectID = 1, Priority = Priority.Medium, Description = "Description for Task Third", DueDate = DateTime.Now.AddDays(7) };
            Tasks[3] = new TaskDetail { TaskId = 4, Summary = "Task Fourth for Project One", ProjectID = 1, Priority = Priority.Low, Description = "Description for Task Fourth", DueDate = DateTime.Now.AddDays(9) };
            Tasks[4] = new TaskDetail { TaskId = 5, Summary = "Task Fifth for Project One", ProjectID = 1, Priority = Priority.Medium, Description = "Description for Task Fifth", DueDate = DateTime.Now.AddDays(12) };
            Tasks[5] = new TaskDetail { TaskId = 6, Summary = "Task First for Project Two", ProjectID = 2, Priority = Priority.Critical, Description = "Description for Task First", DueDate = DateTime.Now.AddDays(3) };
            Tasks[6] = new TaskDetail { TaskId = 7, Summary = "Task Second for Project Two", ProjectID = 2, Priority = Priority.High, Description = "Description for Task Second", DueDate = DateTime.Now.AddDays(5) };
            Tasks[7] = new TaskDetail { TaskId = 8, Summary = "Task Third for Project Two", ProjectID = 2, Priority = Priority.Medium, Description = "Description for Task Thrid", DueDate = DateTime.Now.AddDays(8) };
            Tasks[8] = new TaskDetail { TaskId = 9, Summary = "Task Fourth for Project Two", ProjectID = 2, Priority = Priority.Low, Description = "Description for Task Fourth", DueDate = DateTime.Now.AddDays(10) };
            Tasks[9] = new TaskDetail { TaskId = 10, Summary = "Task Fifth for Project Two", ProjectID = 2, Priority = Priority.Medium, Description = "Description for Task Fifth", DueDate = DateTime.Now.AddDays(13) };


        }

        public ProjectDetail GetProject(int Id)
        {
            return Projects.FirstOrDefault(p => p.ProjectID == Id);
        }

        public TaskDetail[] GetTasks(int projectId)
        {
            TaskDetail[] tasks = null;

            lock (Tasks)
            { 
             tasks = Tasks.Where(t => t.ProjectID == projectId).ToArray();
            }

            return tasks;
        }

        public void SaveTaskDetail(TaskDetail task)
        {
            TaskDetail existingTask = null;
            lock (Tasks)
            {
                existingTask = Tasks.SingleOrDefault(t => t.TaskId == task.TaskId);

                existingTask.IsComplete = task.IsComplete;

                if (existingTask.IsComplete)
                    existingTask.CompletedDate = DateTime.Now;
            }

            Parallel.ForEach(subscription, ( callback) =>
           {
               if (((ICommunicationObject)callback).State == CommunicationState.Opened)
               {
                   callback.OnTaskDetailChanged(existingTask);
               }
               else
               {
                   subscription.Remove(callback);
               }
           });
        }

        public void SubscribeTaskChangeEvent()
        {
            IKanbanServiceCallback callback = OperationContext.Current.GetCallbackChannel<IKanbanServiceCallback>();

            if (!subscription.Contains(callback))
                subscription.Add(callback);

        }

        public void UnSubscribeTaskChangeEvent()
        {
            IKanbanServiceCallback callback = OperationContext.Current.GetCallbackChannel<IKanbanServiceCallback>();

            if (subscription.Contains(callback))
                subscription.Remove(callback);
        }
    }
}
