using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Data
{
    public class DBImage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
