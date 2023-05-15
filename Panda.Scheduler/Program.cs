using Panda.Models;
using Panda.Schedulers;
using System.Linq;

namespace Panda.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {

            RieltorScheduler.Start();

            System.Console.Read();
        }
    }
}
