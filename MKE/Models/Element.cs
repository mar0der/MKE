using MKE.Services;

namespace MKE.Models
{
    public class Element
    {
        public int Id { get; private set; }
        public int StartNodeId { get; set; } // Replaced Node with Node ID
        public int EndNodeId { get; set; }   // Replaced Node with Node ID
        public Material Material { get; set; }
        public CrossSection CrossSection { get; set; }

        public Element() { }

        public Element(int startNodeId, int endNodeId, Material material, CrossSection crossSection)
        {
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            Id = IdGeneratorService.Instance.GetNextElementId();
            Material = material;
            CrossSection = crossSection;
        }
    }
}