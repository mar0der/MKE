using MKE.Services;

namespace MKE.Models
{
    public class Element
    {
        public int Id { get; private set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public Material Material { get; set; }
        public CrossSection CrossSection { get; set; }

        public Element() { } // Default constructor

        public Element(Node startNode, Node endNode, Material material, CrossSection crossSection)
        {
            StartNode = startNode;
            EndNode = endNode;
            Id = IdGeneratorService.Instance.GetNextElementId(); // Assuming you have a similar mechanism for Elements' IDs

            // Set default Material and Section to null (or you can assign default values if needed)
            Material = material;
            CrossSection = crossSection;
        }

        // ... other methods and properties ...
    }
}
