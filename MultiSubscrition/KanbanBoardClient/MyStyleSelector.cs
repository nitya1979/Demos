using KanbanBoardClient.KanbanSvc;
using System.Windows;
using System.Windows.Controls;

namespace KanbanBoardClient
{
    public class MyStyleSelector : StyleSelector
     {

         public Style CriticalStyle

        { get; set; }

 

        public Style HighStyle

         { get; set; }

         public Style MediumStyle

         { get; set; }

        public Style LowStyle

        { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)

         {

             TaskDetail task = item as TaskDetail;
            if (task != null)

            {

                if (task.Priority == Priority.Critical)

                    return CriticalStyle;

                else if (task.Priority == Priority.High)

                    return HighStyle;

                else if (task.Priority == Priority.Medium)

                    return MediumStyle;

                else if (task.Priority == Priority.Low)
                    return LowStyle;

            }

            return base.SelectStyle(item, container);

         }

     }
}
