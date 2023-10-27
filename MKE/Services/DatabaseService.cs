using Accessibility;
using MKE.Data;
using MKE.Models;
using MKE.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MKE.Services
{
    public class DatabaseService
    {
        #region Private fields
        private Database _currentDatabase;
        private static readonly Lazy<DatabaseService> _instance = new Lazy<DatabaseService>(() => new DatabaseService());
        private readonly EventAggregator _eventAggregator;
        private readonly DatabaseStorageManager _storageManager = DatabaseStorageManager.Instance;
        #endregion

        #region Singleton
        public static DatabaseService Instance => _instance.Value;

        private DatabaseService()
        {
            _eventAggregator = EventAggregator.Instance;
        }
        #endregion

        #region Public Properties
        public Database CurrentDatabase {
            get { return _currentDatabase; }
            set 
            {
                _currentDatabase = value;
                _eventAggregator.Publish(new DatabaseUpdatedMessage(CurrentDatabase));
            } 
        }
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

        public Node GetNodeById(int id)
        {
            return CurrentDatabase?.Nodes.FirstOrDefault(n => n.Id == id);
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

        public IEnumerable<Element> GetAllElements()
        {
            return CurrentDatabase?.Elements ?? Enumerable.Empty<Element>();
        }
        public Element GetElementById(int id)
        {
            return CurrentDatabase?.Elements.FirstOrDefault(e => e.Id == id);
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
        #endregion
    }
}
