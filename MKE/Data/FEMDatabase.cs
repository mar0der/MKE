using MKE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Data
{
    public class FEMDatabase
    {
        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Material> Materials { get; set; } = new List<Material>();

        public List<Element> Elements { get; set; } = new List<Element>();

        public FEMDatabase()
        {
            // Initialization logic here if needed.
        }
    }
}
