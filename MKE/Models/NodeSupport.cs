using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Models
{
    public class NodeSupport
    {
        public int Id { get; set; }  // Unique identifier for the support

        public bool IsFixedInX { get; set; }  // Restrict translation along X
        public bool IsFixedInY { get; set; }  // Restrict translation along Y

        public bool IsRotationFixed { get; set; }  // Restrict rotation in the plane

        public Node AssociatedNode { get; set; }  // The node where this support is applied

        // Constructor
        public NodeSupport()
        {
            // By default, all movements and rotations are allowed (set to false).
            IsFixedInX = false;
            IsFixedInY = false;
            IsRotationFixed = false;
        }

        // You can also have methods to quickly set common support types like pinned, fixed, roller, etc.
    }
}
