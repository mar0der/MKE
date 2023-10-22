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
        private static readonly Lazy<FEMDatabaseService> _instance = new Lazy<FEMDatabaseService>(() => new FEMDatabaseService());

        public static FEMDatabaseService Instance => _instance.Value;

        public FEMDatabase CurrentDatabase { get; set; }

        private readonly EventAggregator _eventAggregator;
        private readonly FEMStorageManager _storageManager = FEMStorageManager.Instance;

        private FEMDatabaseService()
        {
            _eventAggregator = new EventAggregator(); // Adjust as per your event aggregator's requirements
            _eventAggregator.Subscribe<NodeAddedMessage>(OnNodeAdded);
            // Similarly for other relevant events
        }

        private void OnNodeAdded(NodeAddedMessage message)
        {
            if (CurrentDatabase != null)
            {
                CurrentDatabase.Nodes.Add(message.NewNode);
            }
        }

        /// <summary>
        /// Retrieves all nodes from the current database.
        /// </summary>
        /// <returns>A collection of nodes from the current database or an empty collection if the database is null.</returns>
        public IEnumerable<Node> GetAllNodes()
        {
            return CurrentDatabase?.Nodes ?? Enumerable.Empty<Node>();
        }

        public void AddNode(Node newNode)
        {
            if (CurrentDatabase != null)
            {
                CurrentDatabase.Nodes.Add(newNode);
            }
        }
    }
}
