using System.Collections.Generic;
using System.Windows;
using Inventor;

namespace StevenVolckaert.InventorPowerTools.Windows
{
    public partial class GenerateMdfDrawingsWindow : Window
    {
        private GenerateMdfDrawingsViewModel ViewModel
        {
            get { return (GenerateMdfDrawingsViewModel)DataContext; }
            set { DataContext = value; }
        }

        public GenerateMdfDrawingsWindow()
        {
            InitializeComponent();
            ViewModel = new GenerateMdfDrawingsViewModel();

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

        public void Show(IList<PartDocument> mdfParts)
        {
            if (mdfParts == null || mdfParts.Count == 0)
                return;

            ViewModel.MdfParts = mdfParts;
            base.Show();
        }

        private void Hide(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
