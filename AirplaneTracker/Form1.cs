using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirplaneTracker
{
    public partial class Form1 : Form
    {
        #region Properties
        //Map Variables
        Image fileMap;
        Image adjustedFileMap;

        //Plane Variables
        Image filePlane = Image.FromFile(Path.Combine("Images", "plane.png"));
        Image filePlaneHighlighted = Image.FromFile(Path.Combine("Images", "planeHighlighted.png"));
        AirCraft highlightedPlane = null;
        Flights allPlanes = new Flights(); // ← master copy
        BindingList<AirCraft> planeList = new BindingList<AirCraft>(); // <- filtered version of list displayed on UI
        private Dictionary<int, Bitmap> iconRotationCache = new Dictionary<int, Bitmap>();


        //Callsign Variables
        List<string[]> callSignList = new List<string[]>();
        string csvFilePath = Path.Combine("Images", "Callsigns.csv");
        dynamic callsignFont = new Font("Arial", 12, FontStyle.Bold);

        //AppGraphics & Geolocation Variables
        List<AppGraphics> ag = new List<AppGraphics>();
        int agIdx;

        //API Call Variables
        private static readonly HttpClient http = new HttpClient();
        double range = 10;
        double CordsX;
        double CordsY;
        #endregion

        public Form1()
        {
            InitializeComponent();

            InitializeAppGraphics();
        }

        #region Functions

        public async static Task<Flights> GetPlanesAsync(double lat, double lon, double radius)
        {
            string request = "https://api.airplanes.live/v2/point/" + lat.ToString() + "/" + lon.ToString() + "/" + radius.ToString();

            try
            {
                HttpResponseMessage response = await http.GetAsync(request); //remove .ConfigureAwat(false) this was added momentarily
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();

                Flights flight = JsonConvert.DeserializeObject<Flights>(result);

                return flight;
            }
            catch (HttpRequestException ex)
            {
                //return null or an empty Flights object
                return new Flights { error = true, total = 0, ac = new AirCraft[0] };
            }

        }

        public void DrawFlights(Flights flight)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            Bitmap finalImage = new Bitmap(fileMap.Width, fileMap.Height);
            Size size = new Size(filePlane.Height, filePlane.Height);

            using(Graphics g = Graphics.FromImage(finalImage))
            {
                //Draw satellite image onto graphics Obj
                g.DrawImage(fileMap, new Point(0, 0));

                

                //Draw circle depicting radius
                ag[agIdx].DrawCircleRadius(g, range);

                //Draw airplanes
                //NOTE: to ensure planes are at their correct positions, we need
                //too adjust the position of the Rectangle object containing the image.
                //Since the Rectangle XY position is defined by its upper left corner,
                //we must offset this location to the center

                for (int i = 0; i < flight.ac.Count(); i++)
                {                   

                    Bitmap modifiedPlaneImage = (Bitmap)filePlane;
                    string fltCallSign = flight.ac[i].flight;

                    if(showPrivateBox.Checked == false) //original: chkShowPrivate.Checked == false. this uses the front end
                    {
                        if(IsCommercial(fltCallSign) == false)
                        {
                            continue;
                        }
                    }

                    //get pixel coords of upper left corner of Rectangle containing airplane img
                    // ORIGINAL. JUST THIS LINE ---- >Point location = ag[agIdx].ConvertLatLonToPixels(flight.ac[i].lat, flight.ac[1].lon);
                    Point location = ag[agIdx].ConvertLatLonToPixels(flight.ac[i].lat, flight.ac[i].lon);

                    //highlighting a plane when highlited
                    if (highlightedPlane != null)
                    {
                        if (highlightedPlane.flight == flight.ac[i].flight)
                        {
                            modifiedPlaneImage = (Bitmap)filePlaneHighlighted;
                        }
                    }

                    //Calculate offset location that reflects the center of the rectangle and airplane img
                    //ORIGINAL ->> Point offsetLoc = new Point(location.X - Convert.ToInt32(size.Width / 2.0), location.Y - Convert.ToInt32(size.Height / 2.0));
                    Point offsetLoc = new Point(location.X - Convert.ToInt32(size.Width / 2.0), location.Y - Convert.ToInt32(size.Height / 2.0));

                    Rectangle r = new Rectangle(offsetLoc, size);
                    //Rotate Image
                    Bitmap rotImg = ag[agIdx].RotateImage(modifiedPlaneImage, (float)flight.ac[i].track);
                    //Bitmap rotImg = GetRotatedIcon(modifiedPlaneImage, (float)flight.ac[i].track);

                    g.DrawImage(rotImg, r);

                    if(showCallsignsBox.Checked == true)
                    {
                        string callSign = flight.ac[i].flight;
                        Point csLocation = new Point(offsetLoc.X - 20, offsetLoc.Y - 20);

                        g.DrawString(callSign, callsignFont, Brushes.White, csLocation);
                    }

                    //Assign Company given the callsign
                    if(flight.ac[i].company == null)
                    {
                        List<string> companyInfo = GetCompanyFromCallSign(flight.ac[i].flight);

                        flight.ac[i].company = companyInfo[0];
                    }

                    //Get origin and Destination Info
                    if (showOriginAndDestinationBox.Checked == true && flight.ac[i].company != "UNKNOWN")
                    {
                        var apiReturn = AviationStackAPI.GetFlightRouteAsync(flight.ac[i].flight);

                        flight.ac[i].origin = apiReturn.Result.origin;
                        flight.ac[i].destination = apiReturn.Result.destination;
                    }

                }


            }

            sw.Stop();
            long processTime = sw.ElapsedMilliseconds;
            label4.Text = "Process Time: " + processTime.ToString() + " mSec.";
            label2.Text = "# of Flights: " + flight.ac.Count().ToString();
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = finalImage;

        }

        private Bitmap GetRotatedIcon(Bitmap planeImage, float angle)
        {
            // Round to nearest 5 degrees
            int roundedAngle = (int)(Math.Round(angle / 5.0) * 5) % 360;

            if (!iconRotationCache.TryGetValue(roundedAngle, out var rotatedIcon))
            {
                rotatedIcon = ag[agIdx].RotateImage(planeImage, roundedAngle);
                iconRotationCache[roundedAngle] = rotatedIcon;
            }

            return rotatedIcon;
        }

        public List<string> GetCompanyFromCallSign(string callsign)
        {
            List<string> info = new List<string>();
            string code = " ";
            string flightNum = " ";

            if(String.IsNullOrEmpty(callsign) == false)
            {
                code = callsign.Substring(0, 3);
                flightNum = callsign.Substring(3);
            }
            string company;

            try
            {
                string[] codeStrings = callSignList.FirstOrDefault(item => item.Contains(code));
                if(codeStrings.Count() > 0)
                {
                    company = codeStrings[1];
                }
                else
                {
                    company = "UNKNOWN";
                }
            }
            catch(Exception)
            {
                company = "UNKNOWN";
            }
            info.Add(company);
            info.Add(flightNum);
            return info;
        }

        public bool IsCommercial(string callSign)
        {
            return false;
        }

        public void OutputToUI(Flights flights)
        {

            for (int i = 0; i < flights.ac.Count(); i++)
            {
                if (i < planeList.Count)
                {
                    // Update existing object in place
                    planeList[i].flight = flights.ac[i].flight;
                    planeList[i].desc = flights.ac[i].desc;
                    planeList[i].gs = flights.ac[i].gs;
                    planeList[i].alt_baro = flights.ac[i].alt_baro;
                    planeList[i].company = flights.ac[i].company;
                    planeList[i].origin = flights.ac[i].origin;
                    planeList[i].destination = flights.ac[i].destination;
                }
                else
                {
                    // Add new plane
                    planeList.Add(flights.ac[i]);
                }
            }

            // Remove extra entries
            while (planeList.Count > flights.ac.Count())
            {
                planeList.RemoveAt(planeList.Count - 1);
            }

            dataGridPlanes.Refresh();

        }

        private bool IsAnyRadioButtonChecked(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                    return true;

                // Recursively check child containers (e.g., Panels inside Panels)
                if (control.HasChildren && IsAnyRadioButtonChecked(control))
                    return true;
            }

            return false;
        }

        void LoadCSV(string filePath)
        {
            //Using a Parser object to handle when the value in the CSV contains commas
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    // Ensure it has at least 2 columns
                    if (fields.Length >= 2)
                    {
                        callSignList.Add(new[] { fields[0].Trim(), fields[1].Trim() });
                    }
                }
            }
        }

        private void ApplyFilters()
        {
            if (allPlanes == null || allPlanes.ac == null) return;

            string callFilter = txtCallsign.Text.ToLower();
            string companyFilter = txtCompany.Text.ToLower();

            var filtered = allPlanes.ac.Where(p =>
                (p.flight ?? "").ToLower().Contains(callFilter) &&
                (p.company ?? "").ToLower().Contains(companyFilter)
            ).ToList();

            planeList.Clear();
            foreach (var ac in filtered)
                planeList.Add(ac);
        }

        private Flights FilteredResultsFrom(Flights original)
        {
            string filterCallsign = txtCallsign.Text.ToLower();
            string filterCompany = txtCompany.Text.ToLower();

            var filteredAircraft = original.ac.Where(p =>
                (string.IsNullOrEmpty(filterCallsign) || p.flight?.ToLower().Contains(filterCallsign) == true) &&
                (string.IsNullOrEmpty(filterCompany) || p.company?.ToLower().Contains(filterCompany) == true)
            ).ToArray();

            return new Flights { ac = filteredAircraft };
        }

        private void SetupDataGrid()
        {
            dataGridPlanes.SelectionChanged += dataGridView1_SelectionChanged;

            dataGridPlanes.AutoGenerateColumns = false;
            dataGridPlanes.AllowUserToAddRows = false;
            dataGridPlanes.ReadOnly = true;
            dataGridPlanes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridPlanes.DataSource = planeList;

            #region Create Grid Columns
            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "flight", // Must match property name in Aircraft object
                HeaderText = "Callsign",
                Name = "Callsign"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "desc",
                HeaderText = "Model",
                Name = "Model"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "gs",
                HeaderText = "Speed (Knots)",
                Name = "Speed (Knots)"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "alt_baro",
                HeaderText = "Altitude (ft)",
                Name = "Altitude (ft)"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "company",
                HeaderText = "Company",
                Name = "Company"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "origin",
                HeaderText = "Origin",
                Name = "Origin"
            });

            dataGridPlanes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "destination",
                HeaderText = "Destination",
                Name = "Destination"
            });

            #endregion

            dataGridPlanes.ClearSelection();
            dataGridPlanes.Columns["Model"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridPlanes.Columns["Company"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }

        private void SetupTimer()
        {
            updateTimer.Interval = 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        #endregion






        #region Event Handlers

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (newYorkRadioButton.Checked)
            {
                foreach (AppGraphics appg in ag)
                {
                    if (appg.location == "NewYork")
                    {
                        agIdx = ag.FindIndex(p => p.location == appg.location);
                        fileMap = Image.FromFile(appg.folderPath + appg.imageFile);

                        foreach (var marker in appg.geoLocations)
                        {
                            if (marker.placeMarkName == "Center")
                            {
                                CordsX = marker.lat;
                                CordsY = marker.lon;
                            }
                        }

                    }

                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (homeRadioButton.Checked)
            {
                foreach (AppGraphics appg in ag)
                {
                    if (appg.location == "Home")
                    {
                        agIdx = ag.FindIndex(p => p.location == appg.location);
                        fileMap = Image.FromFile(appg.folderPath + appg.imageFile);

                        foreach (var marker in appg.geoLocations)
                        {
                            if (marker.placeMarkName == "Center")
                            {
                                CordsX = marker.lat;
                                CordsY = marker.lon;
                            }
                        }

                    }

                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (homeCloseRadioButton.Checked)
            {
                foreach (AppGraphics appg in ag)
                {
                    if (appg.location == "Home(Close)")
                    {
                        agIdx = ag.FindIndex(p => p.location == appg.location);
                        fileMap = Image.FromFile(appg.folderPath + appg.imageFile);

                        foreach (var marker in appg.geoLocations)
                        {
                            if (marker.placeMarkName == "Center")
                            {
                                CordsX = marker.lat;
                                CordsY = marker.lon;
                            }
                        }

                    }

                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (orlandoRadioButton.Checked)
            {
                foreach (AppGraphics appg in ag)
                {
                    if (appg.location == "Orlando")
                    {
                        agIdx = ag.FindIndex(p => p.location == appg.location);
                        fileMap = Image.FromFile(appg.folderPath + appg.imageFile);

                        foreach (var marker in appg.geoLocations)
                        {
                            if (marker.placeMarkName == "Center")
                            {
                                CordsX = marker.lat;
                                CordsY = marker.lon;
                            }
                        }

                    }

                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (lavalletteRadioButton.Checked)
            {
                foreach (AppGraphics appg in ag)
                {
                    if (appg.location == "Lavallette")
                    {
                        agIdx = ag.FindIndex(p => p.location == appg.location);
                        fileMap = Image.FromFile(appg.folderPath + appg.imageFile);

                        foreach (var marker in appg.geoLocations)
                        {
                            if (marker.placeMarkName == "Center")
                            {
                                CordsX = marker.lat;
                                CordsY = marker.lon;
                            }
                        }

                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            SetupTimer();
            SetupDataGrid();

            RangeDropDown.SelectedIndex = 0;
            label2.Hide();

            LoadCSV(csvFilePath);

        }

        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {

                if(IsAnyRadioButtonChecked(this))
                {
                    label2.Show();

                    // 1. Call API & set master list of planes that we will filter from
                    Flights flights = await GetPlanesAsync(CordsX, CordsY, range);
                    allPlanes = flights;
                    // 2. Draw Planes onto map
                    DrawFlights(flights);
                    // 3. Filter Planes in list based on filters applied on UI & Update data grid
                    Flights filteredPlanes = FilteredResultsFrom(allPlanes);
                    OutputToUI(filteredPlanes);
                    // 4. Redraw map (triggers pictureBox_Paint)
                    pictureBox1.Invalidate();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during update: " + ex.Message);
            }
        }

        private void RangeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            range = Convert.ToDouble(RangeDropDown.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridPlanes.SelectedRows.Count > 0)
            {
                var selectedPlane = (AirCraft)dataGridPlanes.SelectedRows[0].DataBoundItem;
                highlightedPlane = selectedPlane;
                pictureBox1.Invalidate(); // Redraw the map
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        #endregion


    }
}
