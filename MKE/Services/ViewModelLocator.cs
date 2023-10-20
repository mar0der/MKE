using MKE.ViewModels;
using MKE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Services
{
    public static class ViewModelLocator
    {
        private static readonly EventAggregator _eventAggregator = new EventAggregator();

        public static ToolbarViewModel ToolbarViewModel => new ToolbarViewModel(_eventAggregator);

        public static DrawingCanvasViewModel DrawingCanvasViewModel => new DrawingCanvasViewModel(_eventAggregator);

        public static StatusBarViewModel StatusBarViewModel => new StatusBarViewModel(_eventAggregator);

    }
}
