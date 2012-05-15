using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using AD2.Factories;

namespace AD2
{
    [DataContract]
    public class Project : Notifier
    {
        [DataMember]
        public Guid Id { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public void RefreshWorkItems()
        {
            WorkItemFactory wf = new WorkItemFactory();
            wf.GetAll(this, delegate(object sender, WorkItemsCompleteEventArgs args)
            {
                if (!args.Success) return;

                WorkItems.Merge(args.WorkItems);
                NotifyPropertyChanged("WorkItems");
            });
        }

        private ObservableCollection<WorkItem> _workItems;

        public ObservableCollection<WorkItem> WorkItems { get { return _workItems ?? (_workItems = new ObservableCollection<WorkItem>()); } }

        public override bool Equals(object obj)
        {
            return obj is Project ? Equals((Project) obj) : base.Equals(obj);
        }

        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
