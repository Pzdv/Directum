namespace ConsoleApp1
{
    internal class Application
    {
        private readonly List<Meeting> _meetings = new();

        private readonly List<MenuItem> _menuItems = new();

        public void AddMenuItem(MenuItem newMenuItem)
        {
            _menuItems.Add(newMenuItem);
        }

        internal void Run()
        {
            while (true)
            {
                var cts = new CancellationTokenSource();
                Task.Run(() => CheckingMeetingForNotification(cts.Token));

                ShowMenu();

                var commandNumber = GetCommandFromConsole();

                cts.Cancel();
                DoMenuAction(commandNumber);
            }
        }

        private static int GetCommandFromConsole()
        {
            while(true)
            {
                var isParsed = int.TryParse(Console.ReadLine(), out var commandNumber);
                if (isParsed)
                {
                    return commandNumber;
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Неправильно набран номер{Environment.NewLine}");
                }
            }
        }

        private void CheckingMeetingForNotification(CancellationToken cancellationToken)
        {
            while (true)
            {
                var meetingsCount = _meetings.Count;
                for (var i = 0; i < meetingsCount; i++)
                {
                    if(cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var isNotNotifikated = _meetings[i].TryGetNotificationString(out var result);
                    if (isNotNotifikated)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
        }

        private void ShowMenu()
        {
            foreach (var menuItem in _menuItems)
            {
                menuItem.PrintToConsole();
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Для выбора команды введите ее номер");
        }

        private void DoMenuAction(int menuNumber)
        {
            var menuItem = _menuItems.FirstOrDefault(x => x.Numebr == menuNumber);

            if(menuItem == null)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Такого пунка нет в меню");
                Console.WriteLine(Environment.NewLine);
                return;
            }

            menuItem.DoAction(_meetings);
        }
    }
}
