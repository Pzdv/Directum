using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemClose : MenuItem
    {
        public MenuItemClose(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            Environment.Exit(0);
        }
    }
}
