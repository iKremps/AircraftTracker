using System.Collections.Generic;
using System.Drawing;

namespace AirplaneTracker
{
    public class Flights
    {
        public bool error { get; set; }
        public int total { get; set; } //total aircraft returned
        public AirCraft[] ac { get; set; } //list of aircraft
    }

    public class AirCraft
    {
        public string desc { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string alt_baro { get; set; }
        public double gs { get; set; }
        public string r { get; set; } //aircraft registration pulled from DB
        public double track { get; set; } //pointing direction of aircraft in degrees (0 - 359)
        public double trackOld { get; set; }
        public string company { get; set; }
        public string flightNum { get; set; }
        public string flight { get; set; } //callsign of aircraft
        public double distance { get; set; }
        public double speedMPH { get; set; }
        public int? pBoxIDX { get; set; }
        public float imgNetRotationFromZero { get; set; }
        public Point location { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }

    }
}
