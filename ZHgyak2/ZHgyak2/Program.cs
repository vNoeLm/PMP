namespace ZHgyak2
{
    public enum ShipClassType
    {
        Cargo,
        Military,
        Research,
        Mining,
        Colonial,
        Rescue,
    }

    enum CrewStatusType
    {
        Active = 1,
        InCryosleep = 2,
        MissingInAction = 3,
        Deceased = 4
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            FleetHandler fleetHandler = new FleetHandler("weyland-yutani.csv");
            fleetHandler.GenerateReport();
        }
    }
}
