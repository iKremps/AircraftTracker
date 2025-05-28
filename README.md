# AircraftTracker

This is a real-time aircraft tracking application that visualizes flight data over satellite maps using custom-drawn graphics.
• Integrated external APIs (Airplanes.live, AviationStack) to display aircraft metadata, flight paths, and locations.
• Implemented a modular architecture with support for dynamic location loading via KML and PNG files.
• Implemented precise lat/lon to pixel mapping using dynamic bounding box calculations and KML-based geolocation data, enabling accurate
overlay of real-time aircraft positions on satellite map imagery.


Image & Coords:
- Each map image and its relevant coords are utalized from Google Earth, where the placemarks are extracted and read from Google Earth pinpoint files (.kml files)
- To add a new location to the software, you must first create a full set of placemarks (pins): Center, UpperLeft, UpperRight, LowerLeft, LowerRight. Then when clicking on the Center pin, save an image through Google Earth. The image must be saved as a .jpg, and the placemarks as a .kml file
- Place these files into: **\bin\Debug\net5.0-windows\Images**


Coords -> Pixel Translation:
- To ensure the images of the planes are mapped correctly ontop of the main image of a location, a conversion of coordinates into pixels needs to take place. To get these pixel points, you need the original image's resolution, as well as its placemarker coordinates for each corner. You also need the current coordinates of the plane
