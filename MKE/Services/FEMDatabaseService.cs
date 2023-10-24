using MKE.Data;
using MKE.Models;
using MKE.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MKE.Services
{
    public class FEMDatabaseService
    {
        #region Private fields
        private static readonly Lazy<FEMDatabaseService> _instance = new Lazy<FEMDatabaseService>(() => new FEMDatabaseService());
        private readonly EventAggregator _eventAggregator;
        private readonly FEMDatabaseStorageManager _storageManager = FEMDatabaseStorageManager.Instance;
        #endregion

        #region Singleton
        public static FEMDatabaseService Instance => _instance.Value;

        private FEMDatabaseService()
        {
            _eventAggregator = EventAggregator.Instance;
        }
        #endregion

        #region Public Properties
        public FEMDatabase CurrentDatabase { get; set; }
        #endregion

        #region Database Accessing Methods
        /// <summary>
        /// Retrieves all nodes from the current database.
        /// </summary>
        /// <returns>A collection of nodes from the current database or an empty collection if the database is null.</returns>
        public IEnumerable<Node> GetAllNodes()
        {
            return CurrentDatabase?.Nodes ?? Enumerable.Empty<Node>();
        }
        /// <summary>
        /// Adds Node to the database
        /// </summary>
        /// <param name="newNode"></param>
        public void AddNode(Node newNode)
        {
            CurrentDatabase?.Nodes.Add(newNode);
            _eventAggregator.Publish(new DatabaseUpdatedMessage(CurrentDatabase));
        }

        public Element AddElement(Element element)
        {
            CurrentDatabase?.Elements.Add(element);

            // Here, we get the nodes and update their ConnectedElementIds list
            var startNode = CurrentDatabase.Nodes.FirstOrDefault(n => n.Id == element.StartNodeId);
            var endNode = CurrentDatabase.Nodes.FirstOrDefault(n => n.Id == element.EndNodeId);

            startNode?.ConnectedElementIds.Add(element.Id);
            endNode?.ConnectedElementIds.Add(element.Id);

            _eventAggregator.Publish(new DatabaseUpdatedMessage(CurrentDatabase));
            return element;
        }

        public Node GetNodeById(int id)
        {
            return CurrentDatabase?.Nodes.FirstOrDefault(n => n.Id == id);
        }

        public Element GetElementById(int id)
        {
            return CurrentDatabase?.Elements.FirstOrDefault(e => e.Id == id);
        }

        //public Element CreateElement(Node startNode, Node endNode, Material material, CrossSection section)
        //{
        //    var element = new Element(startNode, endNode, material, section);

        //    // Associate the element with its nodes
        //    startNode.ConnectedElements.Add(element);
        //    endNode.ConnectedElements.Add(element);

        //    // Optionally, you can also add the element to the database's element list (if you have one)
        //    CurrentDatabase?.Elements.Add(element);

        //    return element;
        //}

        public IEnumerable<Element> GetAllElements()
        {
            return CurrentDatabase?.Elements ?? Enumerable.Empty<Element>();
        }
        #endregion
    }
}
