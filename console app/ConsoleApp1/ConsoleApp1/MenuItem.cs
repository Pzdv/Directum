namespace ConsoleApp1
{
    internal abstract class MenuItem
    {
        protected readonly string _title;
        protected readonly int _number;

        public MenuItem(int number, string title)
        {
            _number = number;
            _title = title;
        }

        public int Numebr
        {
            get
            {
                return _number;
            }
        }

        public void PrintToConsole()
        {
            Console.WriteLine($"{_number} {_title}");
        }

        internal abstract void DoAction(List<Meeting> meetings);
    }
}