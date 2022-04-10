using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemDelete : MenuItem
    {
        public MenuItemDelete(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            if (meetings.Count == 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Список пуст. Удалять нечего.");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            ShowAll(meetings);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Выберете номер встречи которую хотите удалить");
            Console.WriteLine(Environment.NewLine);

            var deletingMeetingIndex = GetDeletingIndex();

            if (deletingMeetingIndex < 0 || deletingMeetingIndex > meetings.Count - 1)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Встречи с таким номером нет в списке");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            meetings.RemoveAt(deletingMeetingIndex);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Встреча была удалена!");
            Console.WriteLine(Environment.NewLine);

            return;
        }

        private static int GetDeletingIndex()
        {
            while (true)
            {
                var isParsed = int.TryParse(Console.ReadLine(), out var result);
                if (isParsed)
                {
                    return result - 1;
                }
                else
                {
                    Console.WriteLine("Данные введены не верно");
                }
            }
        }

        private static void ShowAll(List<Meeting> meetings)
        {
            var i = 0;
            foreach (var meeting in meetings)
            {
                Console.Write($"{++i} ");
                Console.WriteLine(meeting);
            }
        }
    }
}
