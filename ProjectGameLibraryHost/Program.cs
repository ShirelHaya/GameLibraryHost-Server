using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectGameLibraryHost;
using Model;


namespace ProjectGameLibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost Host = new ServiceHost(typeof(ProjectGameLibraryService.Service1));
            Host.Open();
            ProjectGameLibraryService.Service1 s = new ProjectGameLibraryService.Service1();
            List<subscribers> l = s.GetListSubscribers();
            Console.WriteLine(l.Count);
            Console.ReadLine();
        }
    }
}
