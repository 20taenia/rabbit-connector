using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Core.Messaging
{
    public class EntityListRequest<T> : RequestBase, IPrivateResponseAvailable where T : EntityBase
    {
        private bool _privateResponseRequested = true;

        public EntityListRequest() {  }
        public EntityListRequest(Guid id) : base(id) { }

        public FilterHandle<T> Filter { get; set; }
        public NavigationPropertiesHandle<T> NavigationProperties { get; set; }

        public override string RequestQueue
        {
            get { return TopicQueues.ProductEntitiesList; }
        }

        public bool PrivateResponseRequested
        {
            get
            {
                return _privateResponseRequested;
            }
            set
            {
                //Always true for EntityListRequest
                if (value == false)
                    throw new ArgumentOutOfRangeException("PrivateResponseRequested", value, "PrivateResponseRequested must always be true for EntityListRequests.");
            }
        }

        public string PrivateResponseQueue
        {
            get
            {
                if (Id == null)
                    throw new ArgumentNullException("Id");
                return TopicQueues.ProductEntitiesListPrefix + "." + Id;
            }
        }
    }
}
