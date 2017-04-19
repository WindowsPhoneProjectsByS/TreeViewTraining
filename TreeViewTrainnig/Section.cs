using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewTrainnig
{
    class Section
    {
        public Item folder;
        public List<Item> files;

        public Section()
        {
            files = new List<Item>();
            folder = new Item();
        }
    }
}
