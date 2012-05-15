using System;
using System.Windows;
using System.Windows.Navigation;

namespace Ad2_Dashboard
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();

            AD2.Service.Instance.OnLoginComplete += OnLoginComplete;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry(); // Remove this page from the back queue, so you can't come back here!
            base.OnNavigatedTo(e);
        }

        private void OnLoginComplete(object sender, EventArgs e)
        {
            if (AD2.Service.Instance.IsLoggedOn)
            {
                // Right, navigate to the NEXT page!                
                ContentPanel.RowDefinitions[0].Height = new GridLength(0);
                NavigationService.Navigate(new Uri("/Projects.xaml", UriKind.Relative));
            }
            else
            {
                ContentPanel.RowDefinitions[0].Height = new GridLength(50);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AD2.Service.Instance.Login(tbUsername.Text, tbPassword.Password);
            AppSettings.Save();
        }
    }
}