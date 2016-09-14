using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Generic;
using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Infrastructure.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NLog;

namespace Charon.Engines.PersistenceEngineTester
{
    public class Tester
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _exiting = false;
        private PersistenceService _persistenceService = null;
        
        public void Start()
        {
            _persistenceService = new PersistenceService("red", "Tastyred1");

            //Subscribe to general entity changed events
            _persistenceService.AddAllEntityTypesUpdatedHandler(x => OnAnyEntitiesUpdated(x));

            string input = null;

            while (input != "q" && _exiting == false)
            {
                Console.WriteLine("Ready to send data. Type any key to send, or q to quit.");
                input = Console.ReadLine().ToLower();
                if (input == "q" || _exiting == true)
                    break;

                var owner = new Owner
                {
                    OwnerID = 1,
                    State = ObjectState.Unchanged
                };

                ProductPhysicalAttribute physicalAttribute = new ProductPhysicalAttribute
                {
                    Weight = 200,
                    Height = 100,
                    Length = 300,
                    Width = 50,
                    PackagedWeight = 400,
                    PackagingHeight = 110,
                    PackagingWidth = 100,
                    PackagingLength = 310,
                    State = ObjectState.Unchanged
                };

                Product strangeProduct = new Product
                {
                    Barcode = "X443543454435",
                    Owner = owner,
                    PhysicalAttribute = physicalAttribute,
                    Description = "Strange one",
                    Name = "Whatevs",
                    State = ObjectState.Modified
                };

                ProductCategory category = new ProductCategory
                {
                    Name = "Wij Category",
                    Description = "Just testing categories",
                    ProductAttribute1Name = "Hands",
                    ProductAttribute2Name = "Shoulders",
                    ProductAttribute3Name = "Knees",
                    ProductAttribute4Name = "Toes",
                    CategoryProducts = new List<Product> { strangeProduct },
                    State = ObjectState.Unchanged,
                };

                strangeProduct.Category = category;

                Product nonExistentOwnerProduct = new Product
                {
                    Barcode = "X545646465645",
                    Owner = owner,
                    PhysicalAttribute = physicalAttribute,
                    Description = "Wrong one.",
                    Name = "Jabba the hut",
                    Category = category,
                    State = ObjectState.Added
                };



                //ProductCategory category = new ProductCategory
                //{
                //    Name = "Test category",
                //    Description = null,
                //    ProductAttribute1Name = "Fish Type",
                //    ProductAttribute2Name = "No of Elbows",
                //    ProductAttribute3Name = "Favourite Biscuit",
                //    State = ObjectState.Added
                //};

                //Product okProduct = new Product
                //{
                //    Barcode = "X545646465623",
                //    Owner = ownerNew,
                //    PhysicalAttribute = physicalAttribute,
                //    Description = "Right one.",
                //    Name = "Pricess Leia",
                //    Category = category,
                //    State = ObjectState.Added
                //};

                ////Do some updates
                Guid requestId;
                //requestId = _persistenceService.UpdateEntities(x => OnEntitiesUpdated(x), new List<Owner> { ownerNew, ownerNew2 });
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity update request: {0}", requestId.ToString()));

                requestId = _persistenceService.UpdateEntities(x => OnEntitiesUpdated(x), new List<Product> { nonExistentOwnerProduct });
                _logger.Log(LogLevel.Debug, string.Format("Sending entity update request: {0}", requestId.ToString()));

                //requestId = _persistenceService.UpdateEntities(x => OnEntitiesUpdated(x), new List<Product> { editButDoesntExistProduct, okProduct });
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity update request: {0}", requestId.ToString()));



                ////Grab some warehouse products
                //requestId = _persistenceService.GetEntities<WarehouseProduct>(
                //    response => OnGetEntities(response),
                //    x => true, // Get everything (no filter)
                //    x => x.Warehouse); //Include warehouse nav property
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));

                ////Grab some products
                //requestId = _persistenceService.GetEntities<Product>(
                //    response => OnGetEntities(response),
                //    x => true, // Get everything (no filter)
                //    x => x.Owner,
                //    x => x.MarketplaceProducts,
                //    x => x.Category); //Include warehouse nav property
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));

                //Grab some markeplace products
                requestId = _persistenceService.GetEntities<MarketplaceProduct>(
                    response => OnGetEntities(response),
                    marketplaceProduct => true, // Get everything (no filter)
                    marketplaceProduct => marketplaceProduct.Marketplace,
                    marketplaceProduct => marketplaceProduct.Product,
                    marketplaceProduct => marketplaceProduct.VariationTheme,
                    marketplaceProduct => marketplaceProduct.Product.PhysicalAttribute,
                    marketplaceProduct => marketplaceProduct.Product.Owner);
                _logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));


                requestId = _persistenceService.GetEntities<Product>(
                    response => OnGetEntities(response),
                    product => true, // Get everything (no filter)
                    product => product.Owner, //Include owner nav property
                    product => product.PhysicalAttribute,
                    Product => Product.Category); //Include physical attribute nav property
                _logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));


                ////Grab some markeplace products
                //requestId = _persistenceService.GetPagedEntities<MarketplaceProduct>(
                //    response => OnGetPagedEntities(response),
                //    1,
                //    50,
                //    x => true, // Get everything (no filter)
                //    x => x.Marketplace,
                //    x => x.Product); //Include product nav property
                //_logger.Log(LogLevel.Debug, string.Format("Sending paged entity list request: {0}", requestId.ToString()));


                //requestId = _persistenceService.GetEntities<MarketplaceProduct>(
                //    a => OnGetEntities(a),
                //    b => b.Product.Barcode == "5060248979266",
                //    f => f.Product.Owner.Products,
                //    f => f.Product,
                //    f => f.Product.Owner,
                //    f => f.ProductCondition,
                //    f => f.Product.MarketplaceProducts,
                //    f => f.FulfilmentWarehouse);
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));

                //requestId = _persistenceService.GetEntities<Product>(
                //    a => OnGetEntities(a),
                //    b => true,
                //    null);
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));

                //requestId = _persistenceService.GetEntities<MarketplaceProduct>(
                //    a => OnGetEntities(a),
                //    b => true,
                //    null);
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));

                //requestId = _persistenceService.GetEntities<Country>(
                //    a => OnGetEntities(a),
                //    b => true,
                //    c => c.Warehouses, d => d.StateProvinces);
                //_logger.Log(LogLevel.Debug, string.Format("Sending entity list request: {0}", requestId.ToString()));
            }

            Stop();
        }

        public void Stop()
        {
            _exiting = true;
            _persistenceService.Dispose();
        }

        private void OnEntitiesUpdated<T>(EntitiesChangedResponse<T> response)where T : EntityBase
        {
            if (response.Status == ResponseStatus.Success)
            {
                _logger.Log(LogLevel.Debug, string.Format("EntitiesUpdated {0}: Succeeded", response.Id.ToString()));
            }
            else
            {
                string errors = null;
                response.Errors.ToList().ForEach(x => { errors = errors + x + "/n"; });
                _logger.Log(LogLevel.Error, string.Format("EntitiesUpdated {0}: Failed{2}Errors:{2}{1}", response.Id.ToString(), errors, Environment.NewLine));
            }
        }

        private void OnGetEntities<T>(EntityListResponse<T> response) where T : EntityBase
        {
            if (response.Status == ResponseStatus.Success)
            {
                _logger.Log(LogLevel.Debug, string.Format("GetEntities {0}: Succeeded", response.Id.ToString()));
                _logger.Log(LogLevel.Debug, string.Format("{0} entities of type {1} retrieved", response.Entities.Count, typeof(T).Name));
            }
            else
            {
                string errors = null;
                response.Errors.ToList().ForEach(x => { errors = errors + x + "/n"; });
                _logger.Log(LogLevel.Error, string.Format("EntitiesUpdated {0}: Failed{2}Errors:{2}{1}", response.Id.ToString(), errors, Environment.NewLine));
            }
        }

        private void OnGetPagedEntities<T>(PagedEntityListResponse<T> response) where T: EntityBase
        {
            if (response.Status == ResponseStatus.Success)
            {
                _logger.Log(LogLevel.Debug, string.Format("GetEntities {0}: Succeeded", response.Id.ToString()));
                _logger.Log(LogLevel.Debug, string.Format("{0} entities of type {1} retrieved", response.CollectionPage.Items.Count, typeof(T).Name));
            }
            else
            {
                string errors = null;
                response.Errors.ToList().ForEach(x => { errors = errors + x + "/n"; });
                _logger.Log(LogLevel.Error, string.Format("EntitiesUpdated {0}: Failed{2}Errors:{2}{1}", response.Id.ToString(), errors, Environment.NewLine));
            }
        }

        private void OnAnyEntitiesUpdated(object response)
        {
            Type type = response.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EntitiesChangedResponse<>))
            {
                var containedType = type.GetGenericArguments()[0];

                //var method = typeof(Program).GetMethod("GetResponse", BindingFlags.Static | BindingFlags.NonPublic);
                //method = method.MakeGenericMethod(containedType);
                //var newResponse = method.Invoke(null, new object[] { updates });

                var responseBase = response as IResponseBase;

                if (responseBase.Status == ResponseStatus.Success)
                {
                    dynamic dynamicResponse = response;
                    _logger.Log(LogLevel.Debug, string.Format("AnyEntitiesUpdated {0} Received", responseBase.Id.ToString()));
                    _logger.Log(LogLevel.Debug, string.Format("{0} entities of type {1} updated", dynamicResponse.EntitiesUpdated.Count, containedType.Name));
                }
                else
                {
                    string errors = null;
                    responseBase.Errors.ToList().ForEach(x => { errors = errors + x + "/n"; });
                    _logger.Log(LogLevel.Error, string.Format("EntitiesUpdated {0}: Failed{2}Errors:{2}{1}", responseBase.Id.ToString(), errors, Environment.NewLine));
                }

            }
        }
    }
}
