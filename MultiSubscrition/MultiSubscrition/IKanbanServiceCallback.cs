using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MultiSubscrition
{
    
    [ServiceContract]
    public interface IKanbanServiceCallback
    {
        [OperationContract]
        void OnTaskDetailChanged(TaskDetail task);
    }
}
