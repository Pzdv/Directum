using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemSave : MenuItem
    {
        public MenuItemSave(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "meetings");
            Directory.CreateDirectory(path);

            using (var sw = new StreamWriter(path + @"\meetings.txt", false))
            {
                foreach (var item in meetings)
                {
                    sw.WriteLine(item);
                }
            }
            Console.WriteLine($"Путь к сохраненному файлу: {path}");
        }
    }
}
