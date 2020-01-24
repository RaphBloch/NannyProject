using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System.Threading;

namespace BE
{
    public class DistanceClass
    {
        //  fields
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Distance { get; set; }
        public bool flag;

        //  constructor
        public DistanceClass(string a1, string a2)
        {
            Address1 = a1;
            Address2 = a2;
            flag = false;

            Thread ThreadDistance = new Thread(SetDistance);
            ThreadDistance.Start();
            ThreadDistance.Join();

            if (flag)
                throw new Exception("The program can't calculate the distances for now, please try later.");
        }

        /// <summary>
        /// To set distance between addresses
        /// </summary>
        private void SetDistance()
        {
            try
            {
                Distance = GoogleDistance(Address1, Address2);
            }
            catch (Exception)
            {
                flag = true;
            }
           
        }

        /// <summary>
        /// function to calculate with google maps the distance between two adresses
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        private int GoogleDistance(string src, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = src,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;
        }
    }
}
