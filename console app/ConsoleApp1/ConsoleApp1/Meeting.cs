
namespace ConsoleApp1
{
    internal class Meeting
    {
        private readonly int _notifyFor;
        public Meeting(string name, DateTime start, DateTime end, int notifyFor)
        {
            Name = name;
            Start = start;
            End = end;
            _notifyFor = notifyFor;
        }

        public string Name { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public bool IsNotificated { get; private set; }

        public bool TryGetNotificationString(out string result)
        {
            result = string.Empty;
            if (IsNotificated)
            {
                return false;
            }

            var isTimeToNotification = Start.Subtract(DateTime.Now).Minutes < _notifyFor;

            if(isTimeToNotification)
            {
                IsNotificated = true;
                result = $"В {Start:t} начинается {Name}";
                return true;
            }
            return false;
        }
        
        public override string ToString()
        {
            return $"Название встречи: {Name} | Дата и время: {Start:g}  |  Дата и время окончания: {End:g}";
        }
    }
}
