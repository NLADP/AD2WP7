using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AD2;

namespace Ad2_Dashboard
{
    public partial class Projects
    {
        // Constructor
        public Projects()
        {
            InitializeComponent();

            Service.Instance.RefreshProjects();
            Loaded += ProjectsLoaded;
        }

        private void ProjectsLoaded(object sender, RoutedEventArgs e)
        {
            MainListBox.ItemsSource = Service.Instance.Projects;
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            Project selectedProject = MainListBox.SelectedValue as Project;
            if (selectedProject != null) NavigationService.Navigate(new Uri("/WorkItems.xaml?project=" + selectedProject.Id, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }
    }
}