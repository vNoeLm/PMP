using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHgyak2
{
    internal class FleetHandler
    {
        private List<Spaceship> _fleet = new List<Spaceship>();
        private DateTime _currentDate;

        public FleetHandler(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            _currentDate = DateTime.Parse(sr.ReadLine());
            while (!sr.EndOfStream)
            {
                _fleet.Add(new Spaceship(sr.ReadLine()));
            }
            sr.Close();
            fs.Close();
        }

        public int TotalShipCount()
        {
            return _fleet.Count;
        }

        public bool HasAnyDeceasedCrew()
        {
            for (int i = 0; i < _fleet.Count; i++)
            {
                if (_fleet[i].CrewStatus == CrewStatusType.Deceased)
                {
                    return true;
                }
            }
            return false;
        }

        public double AverageCargo(ShipClassType shipClass)
        {
            double cargoSum = 0;
            int shipCount = 0;
            for (int i = 0; i < _fleet.Count; i++)
            {
                if (_fleet[i].ShipClass == shipClass)
                {
                    cargoSum += _fleet[i].CargoTonnage;
                    shipCount++;
                }
            }
            return cargoSum / shipCount;
        }

        public Spaceship[] GetShipsByRisk()
        {
            List<Spaceship> riskyShips = new List<Spaceship>();
            List<Spaceship> safeShips = new List<Spaceship>();
            for (int i = 0; i < _fleet.Count; i++)
            {
                if ((_fleet[i].ShipClass == ShipClassType.Cargo || _fleet[i].ShipClass == ShipClassType.Research) && _fleet[i].NeedsRescue(_currentDate))
                {
                    riskyShips.Add(_fleet[i]);
                }
                else
                {
                    safeShips.Add(_fleet[i]);
                }
            }
            riskyShips.AddRange(safeShips);
            return riskyShips.ToArray();
        }

        public void GenerateReport()
        {
            Console.WriteLine($"Current Date is {_currentDate}");
            Console.WriteLine($"Total ship Count: {TotalShipCount()}");
            Console.WriteLine($"Average cargo by ship types:");
            for (int i = 0; i < Enum.GetValues(typeof(ShipClassType)).Length; i++)
            {
                ShipClassType shipClass = (ShipClassType)i;
                Console.WriteLine($"\t{shipClass}:  {AverageCargo(shipClass)}");
            }
            Console.WriteLine("Detailed info sorted by risk:");
            Spaceship[] shipsByRisk = GetShipsByRisk();
            for (int i = 0; i < shipsByRisk.Length; i++)
            {
                string statusReport = shipsByRisk[i].GetStatusReport(_currentDate);
                Console.WriteLine(statusReport);
            }
        }
    }
}