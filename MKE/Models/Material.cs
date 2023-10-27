using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Models
{
    public class Material
    {
        public int Id { get; set; }  // Unique identifier for the material

        // General Parameters
        public string Name { get; set; }
        public string Type { get; set; }  // E.g., Steel, Concrete, Wood, etc.
        public string Description { get; set; }  // A brief description or remarks

        // Mechanical Properties
        public double ElasticModulus { get; set; }  // Young's Modulus (E)
        public double ShearModulus { get; set; }  // (G)
        public double PoissonRatio { get; set; }  // (ν)

        // Strength Properties
        public double YieldStrength { get; set; }  // (Fy)
        public double TensileStrength { get; set; }  // (Fu)
        public double CompressiveStrength { get; set; }
        public double ShearStrength { get; set; }

        // Density and Weight
        public double Density { get; set; }  // Mass per unit volume
        public double WeightPerUnitVolume { get; set; }  // Weight per unit volume, e.g., kN/m^3

        // Thermal Properties
        public double CoefficientOfThermalExpansion { get; set; }  // α
        public double ThermalConductivity { get; set; }  // k

        // Other Relevant Properties (Depending on the type of material)
        public double DampingRatio { get; set; }  // Used for dynamic analysis
        public double FatigueStrength { get; set; }  // For materials prone to fatigue

        // Constructor
        public Material()
        {
            // Initialization logic can be placed here if needed.
        }
    }
}
