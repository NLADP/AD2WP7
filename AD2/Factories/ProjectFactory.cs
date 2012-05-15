using System;

namespace AD2.Factories
{
    internal class ProjectCompleteEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public Project[] Projects { get; set; }

        public ProjectCompleteEventArgs(bool success, Project[] projects)
        {
            Success = success;
            Projects = projects;
        }
    }

    internal class ProjectFactory
    {
        private const string Url = "/Project";

        public delegate void ProjectCompleteHandler(object sender, ProjectCompleteEventArgs e);

        public void GetAll(ProjectCompleteHandler handler)
        {
            WebService.Get(Url, delegate(bool success, string result)
                                    {
                                        Project[] projects = success ? Serializer.Get<Project[]>(result) : null;
                                        handler(this, new ProjectCompleteEventArgs(success, projects));
                                    });
        }
    }
}
