using KanbanBoardClient.KanbanSvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ServiceModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;

namespace KanbanBoardClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class MainWindowViewModel : IKanbanServiceCallback, INotifyPropertyChanged
    {
        ICommand _saveCommand = null;
        private TaskDetail[] tasks = null;
        KanbanServiceClient kanbanProxy = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void GetTasks()
        {

            if (kanbanProxy == null)
            {
                InstanceContext context = new InstanceContext(this);
                kanbanProxy = new KanbanServiceClient(context);
                kanbanProxy.SubscribeTaskChangeEvent();
            }
            //tasks.Add(new TaskDetail { TaskId = 1, Summary = "Task First for Project One", ProjectID = 1, Priority = Priority.Critical, Description = "Description for Task First", DueDate = DateTime.Now.AddDays(3) });
            //tasks.Add(new TaskDetail { TaskId = 2, Summary = "Task Second for Project One", ProjectID = 1, Priority = Priority.High, Description = "Description for Task Second", DueDate = DateTime.Now.AddDays(5) });
            //tasks.Add(new TaskDetail { TaskId = 3, Summary = "Task Third for Project One", ProjectID = 1, Priority = Priority.Medium, Description = "Description for Task Third", DueDate = DateTime.Now.AddDays(7) });
            //tasks.Add(new TaskDetail { TaskId = 4, Summary = "Task Fourth for Project One", ProjectID = 1, Priority = Priority.Low, Description = "Description for Task Fourth", DueDate = DateTime.Now.AddDays(9) });
            //tasks.Add(new TaskDetail { TaskId = 5, Summary = "Task Fifth for Project One", ProjectID = 1, Priority = Priority.Medium, Description = "Description for Task Fifth", DueDate = DateTime.Now.AddDays(12) });
            //tasks.Add(new TaskDetail { TaskId = 6, Summary = "Task First for Project Two", ProjectID = 2, Priority = Priority.Critical, Description = "Description for Task First", DueDate = DateTime.Now.AddDays(3) });


            tasks = kanbanProxy.GetTasks(1);

        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(p => this.SaveTask());

                return _saveCommand;
            }
        }

        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = string.Format("Invalid property name :{0}", propertyName);

                throw new Exception(msg);
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void SaveTask()
        {
            TaskDetail modifiedTask = tasks.FirstOrDefault(t => t.IsComplete && t.CompletedDate == DateTime.MinValue);
            kanbanProxy.SaveTaskDetail(modifiedTask);
        }

        public void OnTaskDetailChanged(TaskDetail task)
        {
            //TaskDetail eTask = tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            //eTask.IsComplete = task.IsComplete;
            //eTask.CompletedDate = task.CompletedDate;
            GetTasks();
                OnPropertyChanged("PendingTasks");
                OnPropertyChanged("CompletedTasks");
        }

        public MainWindowViewModel()
        {
            GetTasks();
        }

        public List<TaskDetail> PendingTasks
        {
            get
            {
                return tasks.Where(t => t.IsComplete == false).ToList();
            }
        }

        public List<TaskDetail> CompletedTasks
        {
            get
            {
                return tasks.Where(t => t.IsComplete).ToList();
            }
        }
    }
}
