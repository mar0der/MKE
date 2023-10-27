namespace MKE.Models
{

    public class NodeLoad
    {
        public int Id { get; set; }  // Unique identifier for the nodal load

        public double ForceX { get; set; }  // Force along the X direction
        public double ForceY { get; set; }  // Force along the Y direction

        public double MomentZ { get; set; }  // Moment (or torque) about the Z axis

        public Node AssociatedNode { get; set; }  // The node where this load is applied

        // Constructor
        public NodeLoad()
        {
            // By default, all forces and moments are initialized to zero.
            ForceX = 0.0;
            ForceY = 0.0;
            MomentZ = 0.0;
        }
    }
}
