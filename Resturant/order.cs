using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant
{
    [Serializable]
    class order
    {
        public string phone;
        public List<string[]> list;
        public string id_boy;
        public order(string p,string id,List<string[]> L)
        {
            this.phone = p;
            this.list = L;
            this.id_boy = id;
        
        
        }
    }
}
