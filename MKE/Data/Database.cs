using MKE.Models;
using System.Collections.Generic;

namespace MKE.Data
{
    public class Database
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Material> Materials { get; set; } = new List<Material>();

        public List<Element> Elements { get; set; } = new List<Element>();

        public Database()
        {
        }
    }
}
