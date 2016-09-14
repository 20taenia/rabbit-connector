using Charon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Charon.Core.Messaging
{
    public class PagedEntityListResponse<T> : EntityListResponse<T>, IDTOListIncludeProperties, IDTOList where T : EntityBase
    {
        private CollectionPage<T> _collectionPage = null;

        public new List<T> Entities
        {
            get
            {
                if (_collectionPage == null)
                    _collectionPage = new CollectionPage<T>();

                return _collectionPage.Items.ToList();
            }
            set
            {
                if (_collectionPage != null)
                    _collectionPage.Items = value;
                else
                {
                    _collectionPage = new CollectionPage<T>();
                    _collectionPage.Items = value;
                }
            }
        }

        object[] IDTOList.Entities
        {
            get
            {
                if (_collectionPage == null)
                    return null;
                else
                    return _collectionPage.Items.ToArray();
            }
            set
            {
                var entities = new List<T>();

                if (value != null && value.Count() > 0)
                {
                    foreach (var item in value)
                    {
                        var entity = item as T;
                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }

                Entities = entities;
            }
        }

        public CollectionPage<T> CollectionPage
        {
            get
            {
                return _collectionPage;
            }
            set
            {
                _collectionPage = value;
            }
        }
    }
}
