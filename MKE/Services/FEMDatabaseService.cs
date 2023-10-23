using MKE.Data;
using MKE.Models;
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
        }

        public Element CreateElement(Node startNode, Node endNode, Material material, CrossSection section)
        {
            var element = new Element(startNode, endNode, material, section);

            // Associate the element with its nodes
            startNode.ConnectedElements.Add(element);
            endNode.ConnectedElements.Add(element);

            // Optionally, you can also add the element to the database's element list (if you have one)
            CurrentDatabase?.Elements.Add(element);

            return element;
        }

        public IEnumerable<Element> GetAllElements()
        {
            return CurrentDatabase?.Elements ?? Enumerable.Empty<Element>();
        }
        #endregion
    }
}
