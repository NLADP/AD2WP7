using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AD2;

namespace Ad2_Dashboard
{
    public partial class WorkItems
    {
        private Project project;

        // Constructor
        public WorkItems()
        {
            InitializeComponent();

            Loaded += WorkItemsLoaded;
        }

        private void WorkItemsLoaded(object sender, RoutedEventArgs e)
        {
            // Set the data context of the listbox control to the projects overview
            MainListBox.ItemsSource = project.WorkItems;
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string projectId;
            if (NavigationContext.QueryString.TryGetValue("project", out projectId))
            {
                // Find the project
                Guid pId = Guid.Parse(projectId);
                project = Service.Instance.Projects.FirstOrDefault(p => p.Id == pId);
                if (project == null)
                {
                    NavigationService.GoBack();
                    return;
                }

                project.RefreshWorkItems();
            }
            else
                NavigationService.GoBack();
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            AD2.WorkItem wi = MainListBox.SelectedValue as AD2.WorkItem;
            if (wi != null) NavigationService.Navigate(new Uri("/WorkItem.xaml?project= " + project.Id + "&workitem=" + wi.Id, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }
    }
}