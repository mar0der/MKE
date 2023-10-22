namespace MKE.Models.Messages
{
    public class NodeAddedMessage
    {
        public Node NewNode { get; }

        public NodeAddedMessage(Node newNode)
        {
            NewNode = newNode;
        }
    }
}
