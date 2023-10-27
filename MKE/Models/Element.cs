using MKE.Services;
using System.Text.Json.Serialization;

namespace MKE.Models
{
    public class Element
    {
        public int Id { get; set; }
        public int StartNodeId { get; set; } // Replaced Node with Node ID
        public int EndNodeId { get; set; }   // Replaced Node with Node ID
        public Material Material { get; set; }
        public CrossSection CrossSection { get; set; }
        [JsonIgnore]
        public Node StartNode => DatabaseService.Instance.GetNodeById(StartNodeId);
        [JsonIgnore]
        public Node EndNode => DatabaseService.Instance.GetNodeById(EndNodeId);


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