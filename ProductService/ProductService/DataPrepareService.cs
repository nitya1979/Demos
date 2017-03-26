using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrepareService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class DataPrepareService : IDataPrepareService
    {
        public void ProcessData(RecordData[] records)
        {
            IDataPrepareServiceCallBack callback = OperationContext.Current.GetCallbackChannel<IDataPrepareServiceCallBack>();

            IContextChannel channel = OperationContext.Current.Channel;

            Task.Factory.StartNew(() =>
            {
                foreach (RecordData row in records)
                {
                    Thread.Sleep(1000);
                    
                    if( channel.State == CommunicationState.Opened)
                        callback.OnRecordProcessed(row);    
                }
            });
        }
    }
}
