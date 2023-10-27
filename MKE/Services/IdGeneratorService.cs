using MKE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Services
{
    public class IdGeneratorService
    {
        private static readonly Lazy<IdGeneratorService> _instance = new Lazy<IdGeneratorService>(() => new IdGeneratorService());
        public static IdGeneratorService Instance => _instance.Value;

        private int _currentMaxNodeId = 0;
        private int _currentMaxElementId = 0;

        private IdGeneratorService() { }

        public void InitializeWithDatabase(Database database)
        {
            _currentMaxNodeId = (database?.Nodes?.Any() ?? false) ? database.Nodes.Max(node => node.Id) : 0;
            _currentMaxElementId = (database?.Elements?.Any() ?? false) ? database.Elements.Max(element => element.Id) : 0;
        }

        public int GetNextNodeId()
        {
            _currentMaxNodeId++;
            return _currentMaxNodeId;
        }

        public int GetNextElementId()
        {
            _currentMaxElementId++;
            return _currentMaxElementId;
        }
    }
}
