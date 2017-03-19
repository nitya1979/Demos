using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrepareService
{
    [ServiceContract(CallbackContract =typeof(IDataPrepareServiceCallBack))]
    public interface IDataPrepareService
    {
        [OperationContract(IsOneWay = true)]
        void ProcessData(RecordData[] records);
    }
}
