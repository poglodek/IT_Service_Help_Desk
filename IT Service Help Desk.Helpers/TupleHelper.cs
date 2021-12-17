using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Service_Help_Desk.Helpers
{
    public class TupleHelper
    {
        public (string, string, string, bool) CreateTuple(string item1,string item2, string item3, bool item4)
        {
            return (item1, item2, item3, item4);
        }
    }
}
