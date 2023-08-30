using Avalonia.Controls;
using System.Diagnostics;

namespace Orchestrator.Views
{
    public partial class MainGrid : UserControl
    {
        public bool IsClicked {  get; set; }
        private GridLength _firstColumn;

        public GridLength FirstColumn
        {
            get { return _firstColumn; }
            set {  }
        }

        public MainGrid()
        {
            InitializeComponent();
            _firstColumn = new GridLength(75, GridUnitType.Star);
            //menu_button.Click += Menu_button_Click;
        }

        private void Menu_button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Debug.WriteLine(IsClicked);
            
            if (!IsClicked)
            {
                grid.Children[0].Width = 100;
                IsClicked = true;
            }
            else
            {
                grid.Children[0].Width = 50;
                IsClicked = false;
            }
        }

        private void NotifyToggleFullScreen(bool isToggleExpansion)
        {
            if (isToggleExpansion)
            {
                FirstColumn = new GridLength(0, GridUnitType.Auto);
                //SecondColumn = new GridLength(100, GridUnitType.Star);
            }
            else
            {
                FirstColumn = new GridLength(75, GridUnitType.Star);
                //SecondColumn = new GridLength(25, GridUnitType.Star);
            }
        }
    }
}
