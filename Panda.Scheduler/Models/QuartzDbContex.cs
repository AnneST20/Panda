using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Scheduler.Models
{
    class QuartzDbContex : DbContext
    {

        public QuartzDbContex()
            : base("QuartzDb")
        {
        }
    }
}
