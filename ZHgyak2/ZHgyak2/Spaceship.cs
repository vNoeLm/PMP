using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHgyak2
{
    internal class Spaceship
    {
        string _name;
        ShipClassType _shipClass;
        int _crewCapacity;
        CrewStatusType _crewStatus;
        int _cargoTonnage;
        DateTime _lastMessage;

        public string Name
        {
            get { return _name; }
        }

        public ShipClassType ShipClass
        {
            get { return _shipClass; }
        }

        public int CrewCapacity
        {
            get { return _crewCapacity; }
        }

        public CrewStatusType CrewStatus
        {
            get { return _crewStatus; }
        }

        public int CargoTonnage
        {
            get { return _cargoTonnage; }
        }

        public DateTime LastMessage
        {
            get { return _lastMessage; }
        }

        public Spaceship(string line)
        {
            string[] lineDB = line.Split(';');
            _name = lineDB[0];
            _shipClass = (ShipClassType)Enum.Parse(typeof(ShipClassType), lineDB[1]);
            _crewCapacity = int.Parse(lineDB[2]);
            _crewStatus = (CrewStatusType)Enum.Parse(typeof(CrewStatusType), lineDB[3]);
            _cargoTonnage = int.Parse(lineDB[4]);
            _lastMessage = DateTime.Parse(lineDB[5]);
        }

        private int DaysSinceLastMessage(DateTime date)
        {
            return (date - LastMessage).Days;
        }

        public bool NeedsRescue(DateTime date)
        {
            return DaysSinceLastMessage(date) > 30 && _crewStatus == CrewStatusType.Active || DaysSinceLastMessage(date) > 3650 && _crewStatus == CrewStatusType.InCryosleep;
        }

        public override string ToString()
        {
            return $"{Name} {ShipClass}";
        }

        public string GetStatusReport(DateTime date)
        {
            return $"=== {ToString()} ===\n\tCrew: {CrewCapacity} ({CrewStatus})\n\tLast Message: {DaysSinceLastMessage(date)} days\n\tRescue Needed: {(NeedsRescue(date) ? "Yes" : "No")}";
        }
    }
}
