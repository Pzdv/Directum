using System;
using System.Collections.Generic;
using System.Globalization;
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

            var date = GetDateFromConsole();
            var filteredMeetings = meetings.Where(x => x.Start.Date == date.Date).ToList();

            if(filteredMeetings.Count == 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("В выбранный вами день нет записей о встречах. Сохранять нечего.");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            using (var sw = new StreamWriter(path + @"\meetings.txt", false))
            {
                foreach (var item in filteredMeetings)
                {
                    sw.WriteLine(item);
                }
            }
            Console.WriteLine($"Путь к сохраненному файлу: {path}");
        }

        private static DateTime GetDateFromConsole()
        {
            while (true)
            {
                Console.WriteLine("Введите дату, за какой день необходимо сохранить встречи, в формате dd.MM.yyyy");
                var provider = CultureInfo.InvariantCulture;
                var inputLine = Console.ReadLine();
                var isParsed = DateTime.TryParseExact(inputLine, "dd.MM.yyyy", provider, DateTimeStyles.None, out var result);

                if (isParsed)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Данные введены неверно");
                }
            }
        }
    }
}
