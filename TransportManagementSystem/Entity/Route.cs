using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Entity
{    public class Route
        {
            public int RouteID { get; set; }
            public string StartDestination { get; set; }
            public string EndDestination { get; set; }
            public decimal Distance { get; set; }

            public Route() { }

            public Route(int routeID, string start, string end, decimal distance)
            {
                RouteID = routeID;
                StartDestination = start;
                EndDestination = end;
                Distance = distance;
            }
            public override string ToString()
            {
               return $"RouteID: {RouteID}, From: {StartDestination}, To: {EndDestination}, Distance: {Distance} km";
            }

    }
}

