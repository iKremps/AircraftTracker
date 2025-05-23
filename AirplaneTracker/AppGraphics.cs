using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AirplaneTracker
{
    public class AppGraphics
    {
        //This class provides general graphics related functions for the app. It is required that the user places all image and .kml files for all locations in a generic folder location:
        // ("C:\Users\Ian Krempa\source\repos\AirplaneTracker\AirplaneTracker\Images\")

        //Functions in this class include:
        // - Loading image file containing Google Earth map images
        // - Loading .KML files generated from Google Earth describing LAT/LON locations of the pin markers
        // - Converts the .KML data into "GeoLocation" class objects
        // - Adds each "GeoLocation" object to a List<GeoLocation>
        // - Provides methods for: Loading satallite map image, loading and extracting the .KML file, converting LAT/LON values into pixel coords, mapping LAT/LON values to pixel coords, rotating images

        public List<GeoLocation> geoLocations;

        public string folderPath;
        public string location;
        public string imageFile;
        public string kmlFile;
        public string filePath;

        public AppGraphics(string folderPath, string location, string imageFile, string kmlFile)
        {

            this.geoLocations = new List<GeoLocation>();

            this.folderPath = folderPath;
            this.location = location;
            this.imageFile = imageFile;
            this.kmlFile = kmlFile;

            ExtractPlacemarks(folderPath + kmlFile);
        }

        public void ExtractPlacemarks(string filePath)
        {
            try
            {
                //Load .KML
                XDocument kmlDoc = XDocument.Load(filePath);

                //Define namespace for .KML
                XNamespace kmlNamespace = "http://www.opengis.net/kml/2.2";

                //Extract Placemarks
                var placemarks = kmlDoc.Descendants(kmlNamespace + "Placemark");
                foreach (var placemark in placemarks)
                {
                    string name = placemark.Element(kmlNamespace + "name")?.Value;
                    string coordinates = placemark.Element(kmlNamespace + "Point")?.Element(kmlNamespace + "coordinates")?.Value;

                    if (coordinates != null)
                    {
                        //Coords are in the format "long,lat,[altitude]"
                        var coords = coordinates.Split(",");
                        if (coords.Length >= 2)
                        {
                            double longitude = Convert.ToDouble(coords[0]);
                            double latitude = Convert.ToDouble(coords[1]);

                            //Add the name of the folder containing the placemarks, which is the same name as the location (ex: NewYork)
                            geoLocations.Add(new GeoLocation(name, latitude, longitude));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DrawCircleRadius(Graphics g, double radiusMiles)
        {
            if (geoLocations == null || geoLocations.Count == 0)
                throw new InvalidOperationException("GeoLocations list is empty or not initialized.");

            var centerMarker = geoLocations.Find(location => location.placeMarkName == "Center");

            var centerLat = centerMarker.lat;
            var centerLon = centerMarker.lon;

            //Convert center coords to Pixels
            Point center = ConvertLatLonToPixels(centerLat, centerLon);

            //Estimate scale from miles to pixels. 1 degree Long is about 69 miles
            double milesPerDegreeLon = 69.172 * Math.Cos(centerLat * (Math.PI / 180));
            double deltaLon = radiusMiles / milesPerDegreeLon;

            Point edge = ConvertLatLonToPixels(centerLat, centerLon + deltaLon);

            //compute final radius
            double pixelRadius = Math.Sqrt(Math.Pow(center.X - edge.X, 2) + Math.Pow(center.Y - edge.Y, 2));
            double adjustedPixRadius = pixelRadius + (pixelRadius * 15 / 100);

            //Draw
            float diameter = (float)(2 * adjustedPixRadius);
            float topLeftX = (float)(center.X - adjustedPixRadius);
            float topLeftY = (float)(center.Y - adjustedPixRadius);

            using (Pen circlePen = new Pen(Color.DarkGray, 2)) // Thin gray circle
                g.DrawEllipse(circlePen, topLeftX, topLeftY, diameter, diameter);
        }

        public Point ConvertLatLonToPixels(double lat, double lon)
        {

            if (geoLocations == null || geoLocations.Count == 0)
                throw new InvalidOperationException("GeoLocations list is empty or not initialized.");

            double minLat = double.MaxValue;
            double maxLat = double.MinValue;
            double minLon = double.MaxValue;
            double maxLon = double.MinValue;

            //calulate bounds from geoLocation objects
            foreach (var geo in geoLocations)
            {
                if (geo.lat < minLat) minLat = geo.lat;
                if (geo.lat > maxLat) maxLat = geo.lat;
                if (geo.lon < minLon) minLon = geo.lon;
                if (geo.lon > maxLon) maxLon = geo.lon;
            }

            // Step 2: Get image dimensions
            string imagePath = Path.Combine(folderPath, imageFile);
            using (Bitmap bmp = new Bitmap(imagePath))
            {
                int imageWidth = bmp.Width;
                int imageHeight = bmp.Height;

                // Step 3: Map lat/lon to pixel coordinates
                double x = (imageWidth) * (lon - minLon) / (maxLon - minLon);
                double y = (imageHeight) * (1 - (lat - minLat) / (maxLat - minLat)); // flip Y since pixels start in top left with 0,0


                return new Point((int)x, (int)y);
            }
           
        }




        public Bitmap RotateImage(Bitmap inBMP, float angle)
        {
            int w = inBMP.Width;
            int h = inBMP.Height;

            Bitmap rotatedImage = new Bitmap(inBMP.Width, inBMP.Height);
            rotatedImage.SetResolution(inBMP.HorizontalResolution, inBMP.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(inBMP.Width / 2, inBMP.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-inBMP.Width / 2, -inBMP.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(inBMP, new Point(0, 0));
            }

            return rotatedImage;
        }

    }

    public class GeoLocation
    {
        public string placeMarkName { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }

        public GeoLocation(string placeMarkName, double lat, double lon)
        {
            this.placeMarkName = placeMarkName;
            this.lat = lat;
            this.lon = lon;
        }
    }
}
