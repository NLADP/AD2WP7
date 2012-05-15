using System;

namespace AD2.Factories
{
    internal class WorkItemsCompleteEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public WorkItem[] WorkItems { get; set; }

        public WorkItemsCompleteEventArgs(bool success, WorkItem[] workItems)
        {
            Success = success;
            WorkItems = workItems;
        }
    }

    internal class WorkItemFactory
    {
        private const string Url = "/WorkItem?project={0}";

        public delegate void ProjectCompleteHandler(object sender, ProjectCompleteEventArgs e);

        public void GetAll(Project project, EventHandler<WorkItemsCompleteEventArgs> handler)
        {
            WebService.Get(string.Format(Url, project.Id), delegate(bool success, string result)
                                    {
                                        WorkItem[] workitems = success ? Serializer.Get<WorkItem[]>(result) : null;
                                        handler(this, new WorkItemsCompleteEventArgs(success, workitems));
                                    });
        }
    }
}
