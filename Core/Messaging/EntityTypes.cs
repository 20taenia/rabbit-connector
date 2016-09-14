using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Charon.Core.Messaging
{
    public static class EntityTypes
    {
        public static readonly IList<Type> EntityBaseTypes = new List<Type>
            {
                typeof(Address),
                typeof(Country),
                typeof(Currency),
                typeof(Factory),
                typeof(FactoryProduct),
                typeof(FactoryProductionDuration),
                typeof(Language),
                typeof(Marketplace),
                typeof(IntegrationType),
                typeof(MarketplaceProduct),
                typeof(Media),
                typeof(Owner),
                typeof(Product),
                typeof(ProductCondition),
                typeof(ProductCategory),
                typeof(ProductPhysicalAttribute),
                typeof(StateProvince),
                typeof(VariationTheme),
                typeof(Warehouse),
                typeof(WarehouseProduct),
            };

        public static readonly IList<Type> TypesWithExpressions = new List<Type>
            {
                typeof(FilterHandle<>),
                typeof(NavigationPropertiesHandle<>)
            };

        public static readonly IList<Type> EntityRequestTypes = new List<Type>
        {
            typeof(EntityChangeRequest<>),
            typeof(EntityListRequest<>),
            typeof(PagedEntityListRequest<>),
        };

        public static readonly IList<Type> EntityResponseTypes = new List<Type>
        {
            typeof(EntitiesChangedResponse<>),
            typeof(EntityListResponse<>),
            typeof(PagedEntityListResponse<>),
        };

        public static IList<Type> EntityRequestAndResponseTypes
        {
            get
            {
                var result = new List<Type>(EntityRequestTypes).Concat(EntityResponseTypes);
                return result.ToList();
            }
        }
    }
}
