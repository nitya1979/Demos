using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductServiceConsoleClient.ProductSvc;
using System.ServiceModel;
using System.Threading;

namespace ProductServiceConsoleClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ProductServiceCallbackClient : ProductSvc.IDataPrepareServiceCallback, IDisposable
    {
        DataPrepareServiceClient proxy = null;
         
        public void Dispose()
        {
            proxy.Close();
        }

        public void ProcessData()
        {
            InstanceContext context = new InstanceContext(this);

            proxy = new DataPrepareServiceClient(context);

            RecordData[] data1 = new RecordData[20];

            for(int i = 0; i < data1.Length; i++)
            {
                data1[i] = new RecordData
                {
                    FirstName = "First" + (i + 1).ToString(),
                    MiddleName = "Middle" + (i + 1).ToString(),
                    LastName = "Last" + (i + 1).ToString()
                };

            }

            RecordData[] data2= new RecordData[20];

            for (int i = 0; i < data2.Length; i++)
            {
                data2[i] = new RecordData
                {
                    FirstName = "AAA" + (i + 1).ToString(),
                    MiddleName = "BBB" + (i + 1).ToString(),
                    LastName = "CCC" + (i + 1).ToString()
                };

            }

            proxy.ProcessData(data1);
            Console.WriteLine("Data 1 Started");
            Thread.Sleep(5000);
            proxy.ProcessData(data2);
            
            Console.WriteLine("Data 2 Started");
            Console.ReadLine();
        }

        public void OnRecordProcessed(RecordData data)
        {
            Console.WriteLine("Processed : {0} {1} {2}", data.FirstName, data.MiddleName, data.LastName);
        }


    }
}
