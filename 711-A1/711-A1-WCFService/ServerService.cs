using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using _711_A1_WCFService.ServerService;

namespace _711_A1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerService" in both code and config file together.
    public class ServerService : IServerService
    {
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        Stream IServerService.GetFile(string fileName)
        {
            FileStream imageFile = File.OpenRead("\\cache\\" + fileName);
            return imageFile;
        }

        string[] IServerService.GetFileList()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory() + "\\server");
            //return Directory.GetFiles("\\server");
        }
    }

    public class CacheService: ICacheService
    {
        private static ServerServiceClient server;
        //private static StreamWriter cacheLog;

        static CacheService()
        {
            server = new ServerServiceClient();
            //cacheLog = File.AppendText(Directory.GetCurrentDirectory() + "\\cache\\CacheLog.txt");
            /* Currently the above is commented out and a using statement is used with every call to update the log
             * There surely is a more efficient way to do it, but I am doing it this way currently in order to ensure
             * That the file is properly opened and closed every time */
        }

        async Stream ICacheService.GetFile(string fileName)
        {
            using (StreamWriter logout = File.AppendText(Directory.GetCurrentDirectory() + "\\CacheLog.txt"))
            {
                logout.WriteLineAsync(string.Format("\nUser request: Get file {2} at {0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), fileName));
                if (File.Exists("\\cache\\" + fileName))
                {
                    logout.WriteLineAsync(string.Format("Response: Returned cached file {0}", fileName));
                    //return new FileStream("\\cache\\" + fileName, FileMode.Open);
                    FileStream imageFile = File.OpenRead("\\cache\\" + fileName);
                    return imageFile;
                }
                else
                {
                    logout.WriteLineAsync(string.Format("Response: Requested file {0} from server, stored it in cache, and then returned it", fileName));
                    Stream fileToCache = await server.GetFileAsync(fileName);

                    //return fileToCache;
                    //return server.GetFile(fileName);
                    //return new FileStream("\\cache\\" + fileName, FileMode.Open);
                } 
            }
        }

        string[] ICacheService.GetFileList()
        {
            using (StreamWriter logout = File.AppendText(Directory.GetCurrentDirectory() + "\\CacheLog.txt"))
            {
                logout.WriteLineAsync(string.Format("\nUser request: Get file list at {0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString()));
                logout.WriteLineAsync(string.Format("Response: Called GetFileList() on the server, and returned the results"));
                return server.GetFileList();
            }
        }
    }
}
