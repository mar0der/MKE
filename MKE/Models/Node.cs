using MKE.Services;
using System.Collections.Generic;
using System.Windows;

namespace MKE.Models
{
    public class Node
    {
        public int Id { get;  set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<int> ConnectedElementIds { get; set; } = new List<int>(); // This replaces the ConnectedElements list
        public List<NodeLoad> NodeLoads { get; set; } = new List<NodeLoad>();
        public int? SupportID { get; set; }

        public Node() { }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
            Id = IdGeneratorService.Instance.GetNextNodeId();
        }
    }
}