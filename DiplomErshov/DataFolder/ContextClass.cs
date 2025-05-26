using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


namespace DiplomErshov.DataFolder
{
    public partial class DBEntities : DbContext
    {
        private static DBEntities context;

        public static DBEntities GetContext()
        {
            if (context == null)
            {
                context = new DBEntities();
            }
            return context;
        }
        public static void nullContext()
        {
            context = null;
        }
    }
}
