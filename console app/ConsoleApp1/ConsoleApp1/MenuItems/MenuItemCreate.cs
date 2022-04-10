using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemCreate : MenuItem
    {
        public MenuItemCreate(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            var meetingName = GetName();

            while (true)
            {
                Console.WriteLine("Введите дату и время начала встречи в формате dd.MM.yyyy HH:mm");
                var startDate = GetDate();

                Console.WriteLine("Введите дату и время окончания встречи в формате dd.MM.yyyy HH:mm");
                var endDate = GetDate();

                Console.WriteLine("Введите время в минутах, за сколько до начала встречи сделать уведомление");
                var notifyFor = GetNumberFromConsole();

                if (endDate < startDate)
                {
                    Console.WriteLine("Встреча не может закончится до ее начала");
                }
                else
                {
                    var meeting = new Meeting(meetingName, startDate, endDate, notifyFor);

                    if (AnyIntersections(meetings, meeting, out var crossedMeeting))
                    {
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine($"Создаваемая встреча пересекается с уже созданной: {crossedMeeting}. Создание было прервано.");
                        Console.WriteLine(Environment.NewLine);
                        return;
                    }

                    meetings.Add(meeting);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Встреча была добавлена!");
                    Console.WriteLine(Environment.NewLine);
                    return;
                }
            }
        }

        private static int GetNumberFromConsole()
        {
            while (true)
            {
                var isParsed = int.TryParse(Console.ReadLine(), out var result);
                if (isParsed)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Данные введены некорректно");
                }
            }
        }

        private static bool AnyIntersections(List<Meeting> meetings, Meeting meeting, out Meeting? crossedMeeting)
        {
            crossedMeeting = meetings.FirstOrDefault(x => meeting.Start >= x.Start && meeting.Start < x.End ||
                                                          meeting.End > x.Start && meeting.End <= x.End);

            if (crossedMeeting == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static DateTime GetDate()
        {
            while (true)
            {
                var provider = CultureInfo.InvariantCulture;
                var inputLine = Console.ReadLine();
                var isParsed = DateTime.TryParseExact(inputLine, "dd.MM.yyyy HH:mm", provider, DateTimeStyles.None, out var result);

                if (isParsed && result > DateTime.Now)
                {
                    return result;
                }
                else if (isParsed && result < DateTime.Now)
                {
                    Console.WriteLine("Встреча может быть создана только на будущее время");
                }
                else
                {
                    Console.WriteLine("Данные введены неверно");
                }
            }
        }

        private static string GetName()
        {
            Console.WriteLine("Введите название для встречи");

            while (true)
            {
                var result = Console.ReadLine();
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Вы не ввели название");
                }
            }
        }
    }
}
