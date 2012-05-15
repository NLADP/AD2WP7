using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AD2.Factories;

namespace AD2
{
    public class Service : Notifier
    {
        private static Service _instance;

        public static Service Instance { get { return _instance ?? (_instance = new Service()); } }

        public static string BaseUrl { get; set; }

        internal static Uri BaseUri
        {
            get { return new Uri(BaseUrl); }
        }

        public Service()
        {
            Projects = new ObservableCollection<Project>();
        }

        internal static Uri GetUri(string path)
        {
            return new Uri(BaseUri, path);
        }

        private User _currentUser;

        public void Login(string username, string password)
        {
            LoginFactory lf = new LoginFactory();
            lf.Login(username, password, delegate(object sender, LoginCompleteEventArgs args)
                {
                    _currentUser = args.User;

                    Deployment.Current.Dispatcher.BeginInvoke(() => OnLoginComplete(null, EventArgs.Empty));
                });
        }

        public event EventHandler OnLoginComplete;

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsLoggedOn
        {
            get { return _currentUser != null; }
        }

        public void RefreshProjects()
        {
            ProjectFactory pf = new ProjectFactory();
            pf.GetAll(delegate(object sender, ProjectCompleteEventArgs args)
                          {
                              if (!args.Success) return;

                              Projects.Merge(args.Projects);
                              NotifyPropertyChanged("Projects");
                          });
        }

        public ObservableCollection<Project> Projects { get; private set; }
    }
}
