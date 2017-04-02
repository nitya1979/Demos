using System;

namespace KanbanBoardClient
{
    internal class TaskDetail
    {
        
        public int TaskId { get; set; }

        
        public int ProjectID { get; set; }

        
        public string Summary { get; set; }

        
        public string Description { get; set; }

        
        public Priority Priority { get; set; }

        
        public DateTime DueDate { get; set; }

        
        public DateTime CompletedDate { get; set; }

        public bool IsComplete { get; set; }
    }

    enum Priority
    {
        Low,
        Medium,
        High,
        Critical

    }
}