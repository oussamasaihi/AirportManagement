using AM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Services
{
    public interface IFlightService
    {
        /// <summary>
        /// retourne la date d'une Flight
        /// </summary>
        /// <param name="Destination"></param>
        /// <returns></returns>
        List<DateTime> GetDateTimes(string Destination);
        public IList<Passenger> GetThreeOlderTravellers(Flight flight);
        DateTime GetDurationAverage(string destination);
        void GetFlights(string filterType, string filterValue);
        /// <summary>
        /// this method shows the details of each flight when you give a plane as Param
        /// </summary>
        /// <returns></returns>
        
        public IList<Flight> SortFlights();
        public delegate int GetScore(Passenger p);
        public Passenger GetSeniorPassenger(GetScore methan);
    }
}
