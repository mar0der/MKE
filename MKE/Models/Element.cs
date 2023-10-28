using System;

namespace MKE.Models
{
    public class Element
    {
        public int Id { get; set; }

        // Nodes defining the element.
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }

        // Orientation of the element in the global system.
        public double Orientation => ComputeOrientation();

        // Transformation matrix to move between local and global systems.
        public double[,] TransformationMatrix => ComputeTransformationMatrix();

        // ... Other properties related to the element ...

        private double ComputeOrientation()
        {
            // Assuming 2D for simplicity:
            return Math.Atan2(EndNode.Y - StartNode.Y, EndNode.X - StartNode.X);
        }

        private double[,] ComputeTransformationMatrix()
        {
            // For a 2D system, the transformation matrix can be defined using the orientation angle.
            // Here's a basic idea for a 2x2 matrix. This can be extended for more DOFs as needed.

            double cosTheta = Math.Cos(Orientation);
            double sinTheta = Math.Sin(Orientation);

            return new double[,]
            {
                { cosTheta, -sinTheta },
                { sinTheta, cosTheta }
            };
        }

    }
}
