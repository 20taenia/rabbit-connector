using System.Collections.Generic;

namespace Charon.Core.Messaging
{
    public static class TopicQueues
    {
        public static readonly string AllTopics = "#";
        public static readonly string AllEntitiesUpdate = "Charon.*.Update";
        public static readonly string AllProductEntities = "Charon.ProductEntities.*";
        public static readonly string ProductEntitiesUpdate = "Charon.ProductEntities.Update";
        public static readonly string ProductEntitiesList = "Charon.ProductEntities.List";

        public static readonly string ProductEntitiesListPrefix = "Charon.ProductEntities.List";
        public static readonly string ProductEntitiesUpdatedPrefix = "Charon.ProductEntities.Updated";


        public static IEnumerable<string> AllTopicQueues()
        {
            yield return AllTopics;
            yield return AllEntitiesUpdate;
            yield return AllProductEntities;
            yield return ProductEntitiesUpdate;
            yield return ProductEntitiesList;
        }
    }
}
