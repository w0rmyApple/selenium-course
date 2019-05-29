using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFirstAssignment.TheThirdClass
{
    class MenuTitle
    {
        public MenuTitle(string menuItem, string title)
        {
            MenuItem = menuItem;
            Title = title;
        }

        public string MenuItem { get; set; }
        public string Title { get; set; }
    }
}
