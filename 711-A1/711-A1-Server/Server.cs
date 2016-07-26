using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace _711_A1
{
    class Server
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/711A1/");

            ServiceHost selfHost = new ServiceHost(typeof(ServerService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IServerService), new WSHttpBinding(), "ServerService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("The server service is ready");
                Console.WriteLine("Press Enter to terminate service");
                Console.WriteLine();
                Console.ReadLine();

                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.Error.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
