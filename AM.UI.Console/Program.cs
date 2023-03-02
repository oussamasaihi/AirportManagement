// See https://aka.ms/new-console-template for more information
using AM.Core.Domain;
/*
//TP 1.Question7
Console.WriteLine("Bonjour");
Plane plane= new Plane();
plane.Capacity = 300;
plane.ManufactureDate = new DateTime(2000, 1, 1);
plane.MyPlaneType = PlaneType.Boeing;
//TP 1.Question8
Plane plane1 = new Plane(PlaneType.Airbus, 100, new DateTime(2001, 9, 12));
//TP 1.Question9
Plane plane2 = new Plane()
{
    Capacity = 100,
    ManufactureDate = new DateTime(2011, 2, 14)
};
//TP1.Question12.B
Passenger passenger = new Passenger();
Passenger staff =new Staff();
Passenger traveller = new Traveller();
Console.WriteLine(passenger.GetPassengerType());
Console.WriteLine(staff.GetPassengerType());
Console.WriteLine(traveller.GetPassengerType());
//TP1.Question13.C
int calculatedAge = 0;
passenger.GetAge(new DateTime(2000, 1, 1), ref calculatedAge);
Console.WriteLine(calculatedAge);
passenger.BirthDate = new DateTime(2000, 1, 1);*/

internal class Program
{
    private static void Main(string[] args)
    {
        var passengers = new List<Passenger>
            {
                new Passenger
                {
                    FirstName = "oussama",
                    LastName = "Saihi",
                    BirthDate = new DateTime(1950, 1, 1),
                    Flights = new List<Flight>
                    {
                        new Flight{Destination = "Paris"},
                        new Flight {Destination = "New York"},
                    }
                },
                new Passenger
                {
                    FirstName = "jem3i",
                    LastName = "bousebta",
                    BirthDate = new DateTime(1960, 1, 1),
                    Flights = new List<Flight>
                    {
                        new Flight {Destination = "Tunis"},
                        new Flight {Destination = "Paris"},
                    }
                },
                new Passenger
                {
                    FirstName = "essibti",
                    LastName = "boujem3a",
                    BirthDate = new DateTime(1970, 1, 1),
                    Flights = new List<Flight>
                    {
                        new Flight {Destination = "Tunis"},
                    }
                },
            };

        // Méthode de calcul du score basé sur le nombre de vols d'un passager
        int GetFlightCountScore(Passenger passenger)
        {
            return passenger.Flights.Count;
        }

        // Méthode de calcul du score basé sur le nombre de vols de/vers la Tunisie d'un passager
        int GetTunisiaFlightCountScore(Passenger passenger)
        {
            return passenger.Flights.Count(f => f.Destination == "Tunis");
        }

        // Test de la méthode GetSeniorPassenger avec la méthode de calcul de score basé sur le nombre de vols d'un passager
        var seniorPassengerByFlightCount = GetSeniorPassenger(passengers, GetFlightCountScore);
        Console.WriteLine($"Senior passenger by flight count: {seniorPassengerByFlightCount.FirstName} {seniorPassengerByFlightCount.LastName}");

        // Test de la méthode GetSeniorPassenger avec la méthode de calcul de score basé sur le nombre de vols de/vers la Tunisie d'un passager
        var seniorPassengerByTunisiaFlightCount = GetSeniorPassenger(passengers, GetTunisiaFlightCountScore);
        Console.WriteLine($"Senior passenger by Tunisia flight count: {seniorPassengerByTunisiaFlightCount.FirstName} {seniorPassengerByTunisiaFlightCount.LastName}");


        static Passenger GetSeniorPassenger(IEnumerable<Passenger> passengers, Func<Passenger, int> getScoreFunc)
        {
            return passengers.OrderByDescending(getScoreFunc).First();
        }
    }
}