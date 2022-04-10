using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemShowAll : MenuItem
    {
        public MenuItemShowAll(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            if (meetings.Count == 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("В списке еще нет добавленных встреч");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            Console.WriteLine(Environment.NewLine);
            foreach (var meeting in meetings)
            {
                Console.WriteLine(meeting);
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
