using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MultiSubscrition
{
    [ServiceContract( CallbackContract = typeof(IKanbanServiceCallback))]
    public interface IKanbanService
    {
        [OperationContract]
        ProjectDetail GetProject(int Id);

        [OperationContract]
        TaskDetail[] GetTasks(int projectId);

        [OperationContract(IsOneWay = true)]
        void SaveTaskDetail(TaskDetail task);

        [OperationContract]
        void SubscribeTaskChangeEvent();

        [OperationContract]
        void UnSubscribeTaskChangeEvent();
    }
}
