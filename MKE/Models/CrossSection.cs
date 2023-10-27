namespace MKE.Models
{
    public class CrossSection
    {
        public int Id { get; set; }  // Unique identifier for the cross section

        // General Parameters
        public string Name { get; set; }
        public string Type { get; set; }
        public string Material { get; set; }

        // Geometric Parameters
        public double Area { get; set; }
        public double Perimeter { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double FlangeWidth { get; set; }
        public double FlangeThickness { get; set; }
        public double WebThickness { get; set; }
        public double InnerDiameter { get; set; }
        public double OuterDiameter { get; set; }
        public double Radius { get; set; }

        // Centroidal Parameters
        public double CentroidX { get; set; }
        public double CentroidY { get; set; }

        // Moment of Inertia
        public double Ix { get; set; }
        public double Iy { get; set; }

        // Section Modulus
        public double Sx { get; set; }
        public double Sy { get; set; }

        // Torsional Constants
        public double J { get; set; }
        public double Cw { get; set; }

        // Other Parameters
        public double ShearAreaX { get; set; }
        public double ShearAreaY { get; set; }
        public double Zx { get; set; }
        public double Zy { get; set; }
        public double RadiusOfGyrationX { get; set; }
        public double RadiusOfGyrationY { get; set; }
        public double Angle { get; set; }

        // Additional Notes or Remarks
        public string Remarks { get; set; }

        // Constructor
        public CrossSection()
        {
            // Initialization logic can be placed here if needed.
        }
    }

}
