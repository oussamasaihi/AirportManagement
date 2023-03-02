using AM.Core.Domain;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AM.Core.Services
{
    public class FlightService : IFlightService
    {
        private object p;

        IList<Flight> Flights { get; set; }

        public IList<DateTime> GetDateTimes(string Destination)
        {
           
            List<DateTime> dates = new List<DateTime>();
            foreach (var flight in Flights)
            {
                if (flight.Destination == Destination)
                {
                    dates.Add(flight.FlightDate);
                }
            }
            return dates; 

            /*return (IList<DateTime>)(from flight in Flights 
                   where flight.Destination == Destination
                   select flight.FlightDate);
             */
           /* return Flights.Where(f=>f.Destination == Destination)
                .Select(f => Flight.FlightDate).ToList();*/
        }

        List<DateTime> IFlightService.GetDateTimes(string Destination)
        {
            throw new NotImplementedException();
        }
        void GetFlights(string filterType, string filterValue) 
        {

        }
        public void ShowFlightDetails(Plane plane)
        {
            var flightsDetails = plane.Flights
                .Select(f => new { FlightDate = f.FlightDate, Destination = f.Destination });
            foreach (var flight in flightsDetails)
            {
                Console.WriteLine($"Flight date: {flight.FlightDate}, Destination: {flight.Destination}");
            }
        }

        public IList<DateTime> GetFlightDates(string destination)
        {
            return Flights.Where(f => f.Destination == destination)
                          .Select(f => f.FlightDate)
                          .ToList();
        }

        public int GetWeeklyFlightNumber (DateTime date)
        {
            return Flights.Where(f => f.FlightDate >= date && f.FlightDate < date.AddDays(7))
                .Count();
        }

        public double GetDurationAverage(string destination)
        {
            var flightsToDestination = Flights.Where(f => f.Destination == destination);
            if (!flightsToDestination.Any()) return 0;

            var avgDuration = flightsToDestination.Average(f => f.EstimatedDuration);

            return avgDuration;
        }
        public IList<Flight> SortFlights()
        {
            return Flights.OrderByDescending(f => f.EstimatedDuration).ToList();
        }
        public delegate int GetScore(Passenger passenger);
        public Passenger GetSeniorPassenger(List<Passenger> passengers, GetScore getScoreMethod)
        {
            if (passengers == null || passengers.Count == 0)
                return null;

            Passenger seniorPassenger = passengers[0];
            int maxScore = getScoreMethod(seniorPassenger);

            for (int i = 1; i < passengers.Count; i++)
            {
                int score = getScoreMethod(passengers[i]);
                if (score > maxScore)
                {
                    seniorPassenger = passengers[i];
                    maxScore = score;
                }
            }

            return seniorPassenger;
        }

        public IList<Passenger> GetThreeOlderTravellers(Flight flight)
        {
            IOrderedEnumerable<Passenger> passengers = flight.Passengers
                            .OrderByDescending(p => p.BirthDate);
            var olderPassengers = passengers
                .Take(3)
                .ToList();

            return olderPassengers;
        }
        public void ShowGroupedFlights()
        {
            var result = from f in Flights
                         group f by f.Destination;
            foreach (var group in result)
            {
                Console.WriteLine(group.Key);
                foreach (var f in group)
                {
                    Console.WriteLine(f);
                }
            }
            
        }

        void IFlightService.GetFlights(string filterType, string filterValue)
        {
            switch(filterType)
            {
                case "Destination":
                    foreach (var flight in Flights)
                        if (flight.Destination == filterValue)
                        {
                            Console.Write(flight);
                        }
                    break;
                case "FlightId":
                    foreach (var flight in Flights)
                    {
                        if (flight.EstimatedDuration.ToString() == filterValue)
                        {
                            Console.Write(flight);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Error"); break;




            }

        }

        DateTime IFlightService.GetDurationAverage(string destination)
        {
            throw new NotImplementedException();
        }

        Passenger IFlightService.GetSeniorPassenger(IFlightService.GetScore methan)
        {
            throw new NotImplementedException();
        }
    }
}