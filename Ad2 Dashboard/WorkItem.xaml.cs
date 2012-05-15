using System;
using System.Linq;
using System.Windows.Navigation;
using AD2;

namespace Ad2_Dashboard
{
    public partial class WorkItem
    {
        private Project project;
        private AD2.WorkItem workItem;

        // Constructor
        public WorkItem()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string projectId, workItemId;
            if (NavigationContext.QueryString.TryGetValue("project", out projectId) && 
                NavigationContext.QueryString.TryGetValue("workitem", out workItemId))
            {
                Guid pId = Guid.Parse(projectId);
                Guid wId = Guid.Parse(workItemId);

                project = Service.Instance.Projects.FirstOrDefault(p => p.Id == pId);
                if (project != null)
                {
                    workItem = project.WorkItems.FirstOrDefault(w => w.Id == wId);
                }

                if (workItem == null)
                {
                    NavigationService.GoBack();
                    return;
                }

                DataContext = workItem;
            }
        }
    }
}