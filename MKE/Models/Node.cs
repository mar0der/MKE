using MKE.Services;
using System.Collections.Generic;

namespace MKE.Models
{
    public class Node
    {
        public int Id { get; private set; } // Setting the set accessor to private ensures the ID can only be set internally.
        public double X { get; set; }
        public double Y { get; set; }

        public List<NodeLoad> NodeLoads { get; set; } = new List<NodeLoad>();
        public int? SupportID { get; set; }

        public Node() { }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
            Id = IdGeneratorService.Instance.GetNextNodeId();  // Fetch the next available ID for the Node
        }

        // You can add methods to assign loads, etc. here
        // ... other methods and properties ...
    }
}