using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.MenuItems
{
    internal class MenuItemEdit : MenuItem
    {
        public MenuItemEdit(int number, string title) : base(number, title)
        {
        }

        internal override void DoAction(List<Meeting> meetings)
        {
            if (meetings.Count == 0)
            {
                Console.WriteLine("В списке еще нет добавленных встреч");
                return;
            }

            ShowAll(meetings);

            Console.WriteLine("Введите номер встречи которую хотите изменить");

            var meetingIndex = GetIndex(meetings.Count);

            Console.WriteLine("Введите новое название для встречи");
            var newName = GetNewName();

            Console.WriteLine("Введите новую дату и время начала встречи в формате dd.MM.yyyy HH:mm");
            var newStarDate = GetNewDate();

            Console.WriteLine("Введите новую дату и время окончания встречи в формате dd.MM.yyyy HH:mm");
            var newEndDate = GetNewDate();

            Console.WriteLine("Введите время в минутах, за сколько до начала встречи сделать уведомление");
            var notifyFor = GetNumberFromConsole();

            if (newEndDate < newStarDate)
            {
                Console.WriteLine("Встреча не может закончится до ее начала, редактирование было прервано");
                return;
            }

            var newMeeting = new Meeting(newName, newStarDate, newEndDate, notifyFor);

            if (AnyIntersections(meetings, meetingIndex, newMeeting, out var crossedMeeting))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Встреча с новыми данными пересекается с уже созданной: {crossedMeeting}, редактирование было прервано.");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            meetings[meetingIndex] = newMeeting;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Данные успешно изменены");
            Console.WriteLine(Environment.NewLine);
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

        private static bool AnyIntersections(List<Meeting> meetings, int changingMeetingIndex, Meeting meeting, out Meeting? crossedMeeting)
        {
            crossedMeeting = meetings.Where(x => meetings.IndexOf(x) != changingMeetingIndex)
                                     .FirstOrDefault(x => meeting.Start >= x.Start && meeting.Start < x.End ||
                                                          meeting.End <= x.End && meeting.End > x.Start);

            if (crossedMeeting == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static DateTime GetNewDate()
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

        private static void ShowAll(List<Meeting> meetings)
        {
            var i = 0;
            foreach (var meeting in meetings)
            {
                Console.WriteLine($"{++i} " + meeting);
            }
        }

        private static int GetIndex(int range)
        {
            while (true)
            {
                var isParsed = int.TryParse(Console.ReadLine(), out var result);

                if (isParsed && result <= range && result > 0)
                {
                    return result - 1;
                }
                else
                {
                    Console.WriteLine("Данные введены неверно");
                }
            }
        }

        private static string GetNewName()
        {
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
