using Charon.Core.Entities;
using Charon.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.ProductEngine
{
    public static class EntityDepthLimiter<T> where T : EntityBase
    {
        public static IList<T> Limit(IList<T> entities) 
        {
            Type entityType = entities.GetType().GetGenericArguments().Single();

            var ts = new TypeSwitch()
                .Case((MarketplaceProduct x) => entities = LimitMarketplaceProducts(entities));

            //Call TypeSwitch SwitchByType using generic method generation for entityType
            var method = typeof(TypeSwitch).GetMethod("SwitchByType");
            var methodRef = method.MakeGenericMethod(entityType);
            methodRef.Invoke(ts, null);

            return entities;
        }

        private static IList<T> LimitMarketplaceProducts(IList<T> entities)
        {
            entities.ToList().ForEach(x =>
            {
                var mp = x as MarketplaceProduct;

                if (mp.Product != null)
                {
                    mp.Product.WarehouseProducts = null;
                    mp.Product.MarketplaceProducts = null;
                    mp.Product.FactoryProducts = null;
                }

                if (mp.Marketplace != null)
                {
                    mp.Marketplace.MarketplaceProducts = null;
                }

                if (mp.VariationTheme != null)
                {
                    mp.VariationTheme.MarketplaceProductsVariationThemes = null;
                }

                if (mp.ProductCondition != null)
                {
                    mp.ProductCondition.MarketplaceProductsConditions = null;
                }

                if (mp.FulfilmentWarehouse != null)
                {
                    mp.FulfilmentWarehouse.FulfilmentWarehouseMarketplaceProducts = null;
                    mp.FulfilmentWarehouse.WarehouseWarehouseProducts = null;
                }
            });

            return entities;
        }
    }


}
