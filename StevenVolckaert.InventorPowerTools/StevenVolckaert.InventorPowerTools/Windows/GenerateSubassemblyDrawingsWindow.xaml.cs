using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StevenVolckaert.InventorPowerTools.Windows
{
    public partial class GenerateSubassemblyDrawingsWindow : Window
    {
        /* TODO attach double click event of each ListView row to a command in the view model.
         * see http://stackoverflow.com/questions/728205/wpf-listview-attaching-a-double-click-on-an-item-event
         */

        internal GenerateSubassemblyDrawingsViewModel ViewModel
        {
            get { return (GenerateSubassemblyDrawingsViewModel)DataContext; }
            set { DataContext = value; }
        }

        public GenerateSubassemblyDrawingsWindow()
        {
            InitializeComponent();
            ViewModel = new GenerateSubassemblyDrawingsViewModel();

            // TODO find a better solution: don't reuse the window, then there's also no need to intercept the closing event.
            // see http://msdn.microsoft.com/en-us/library/aa972163.aspx
            // and http://stackoverflow.com/a/848106/2314596

            Closing +=
                (sender, e) =>
                {
                    e.Cancel = true;
                    Hide();
                };
        }

        public void Show(List<Assembly> subassemblies)
        {
            if (subassemblies == null || subassemblies.Count == 0)
                return;

            using (var comparer = new HumanReadableStringComparer())
            {
                ViewModel.Subassemblies = subassemblies.OrderBy(x => x.Name, comparer).ToList();
            }

            base.Show();
        }

        private void Hide(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CalculateIsEverythingSelected();
        }
    }
}
