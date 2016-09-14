using Charon.Core.Entities;
using FastMember;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Messaging
{
    public class UpdateConverter
    {
        private List<object> _conversionItems = null;
        private List<object> _convertedItems = null;
        private Dictionary<Type, TypeAccessor> _cachedTypeAccessors = null;
        private Dictionary<Tuple<Type, string>, object> _encounteredObjects = null;

        public List<object> ConvertedItems
        {
            get { return _convertedItems; }
        }

        public void ConvertToDTO(object[] entities)
        {
            _conversionItems = entities.ToList();
            _convertedItems = new List<object>();
            _encounteredObjects = new Dictionary<Tuple<Type, string>, object>();
            _cachedTypeAccessors = new Dictionary<Type, TypeAccessor>();

            ProcessConversions();
        }

        private TypeAccessor GetTypeAccessor(Type type)
        {
            TypeAccessor accessor;
            if (!_cachedTypeAccessors.TryGetValue(type, out accessor))
            {
                accessor = TypeAccessor.Create(type);
                _cachedTypeAccessors.Add(type, accessor);
            }
            return accessor;
        }

        private void ProcessConversions()
        {
            _conversionItems.ForEach(x => ProcessItem(x));
        }

        private void ProcessItem(object item)
        {
            var entity = item;
            var accessor = GetTypeAccessor(entity.GetType());
            var memberSet = accessor.GetMembers();
            var currentType = entity.GetType();
            var convertedEntity = accessor.CreateNew();

            foreach (var member in memberSet)
            {
                var existingPropValue = accessor[entity, member.Name];

                if (existingPropValue == null)
                    continue;

                //Is it one of our entity types or a collection of one
                if (member.Type.IsAssignableFromEntityBase() || member.Type.IsCollectionAssignableFromEntityBase())
                {
                    accessor[convertedEntity, member.Name] = PruneUnmodifiedChildren(existingPropValue);
                }
                else
                {
                    //Its not an entitybase so just assign the object  to our new entity
                    accessor[convertedEntity, member.Name] = accessor[entity, member.Name];
                }
            }

            _convertedItems.Add(convertedEntity);
        }

        private object PruneUnmodifiedChildren(object entity)
        {
            //Quit if null
            if (entity == null)
                return null;

            object newEntity = null;
            var currentType = entity.GetType();
            var accessor = GetTypeAccessor(currentType);

            if (currentType.IsAssignableFromEntityBase())
            {
                object encountered;
                string key = (entity as EntityBase).Key().ToString();
                var typeKey = new Tuple<Type, string>(currentType, key);

                //Just return object if we have already encountered it - prevents infinite loop
                if (_encounteredObjects.TryGetValue(typeKey, out encountered))
                    return encountered;
                //Otherwise add it
                else
                    _encounteredObjects.Add(typeKey, entity);
    
                if (HasModifiedChildren(entity))
                {
                    //Process children
                    var memberSet = accessor.GetMembers();

                    foreach (var member in memberSet)
                    {
                        if (member.Type.IsAssignableFromEntityBase() || member.Type.IsCollectionAssignableFromEntityBase())
                        {
                            accessor[entity, member.Name] = PruneUnmodifiedChildren(accessor[entity, member.Name]);
                        }
                    }

                    newEntity = entity;
                }
                //No unmodified children so check if modified itself
                else if ((entity as EntityBase).State != ObjectState.Unchanged)
                {
                    newEntity = entity;
                }
                //Otherwise just return null
                else
                    newEntity =  null;

                //Update encountered objects with result
                _encounteredObjects[typeKey] = newEntity;
            }
            else if (currentType.IsCollectionAssignableFromEntityBase())
            {
                var collection = (IList)entity;

                if (collection == null || collection.Count == 0)
                    newEntity = null;
                else
                {
                    var newCollection = (IList)accessor.CreateNew();

                    foreach (var item in collection)
                    {
                        var newItem = PruneUnmodifiedChildren(item);
                        newCollection.Add(newItem);
                    }

                    newEntity = newCollection;
                }
            }

            return newEntity;
        }

        private bool HasModifiedChildren(object entity)
        {
            var result = false;
            var currentType = entity.GetType();
            var accessor = GetTypeAccessor(currentType);
            var memberSet = accessor.GetMembers();
            var cachedResults = new Dictionary<Tuple<Type, string>, bool>();
            var cachedObjects = new Dictionary<Tuple<Type, string>, object>();

            foreach (var member in memberSet)
            {
                if (member.Type.IsAssignableFromEntityBase())
                {
                    var child = accessor[entity, member.Name];

                    if (child != null)
                    { 
                        var childType = child.GetType();
                        var childKey = (child as EntityBase).Key().ToString();
                        var childKeyTuple = new Tuple<Type, string>(childType, childKey);

                        if ((child as EntityBase).State != ObjectState.Unchanged)
                        {
                            result = true;
                        }
                        else
                        {
                            //Check result not already cached for this object
                            bool cachedResult;
                            if (cachedResults.TryGetValue(childKeyTuple, out cachedResult))
                            {
                                result = cachedResult;
                            }
                            //If its in our cached objects but not results then we're in a potential infinite loop
                            //So just return results for this and don't go any further
                            else if (cachedObjects.ContainsKey(childKeyTuple))
                            {
                                result = false;
                            }
                            else
                            {
                                cachedObjects.Add(childKeyTuple, child);
                                result = HasModifiedChildren(child);
                            }
                        }

                        //Drop out if we have positive
                        if (result)
                            break;

                        //Add result to cache
                        cachedResults.Add(childKeyTuple, result);
                    }
                }
                else if (member.Type.IsCollectionAssignableFromEntityBase())
                {
                    var childCollection = accessor[entity, member.Name];

                    if (childCollection != null)
                    {
                        var collection = (IList)childCollection;

                        foreach (var item in collection)
                        {
                            if ((item as EntityBase).State != ObjectState.Unchanged)
                            {
                                result = true;
                            }
                            else
                            {
                                result = HasModifiedChildren(item);
                            }

                            //Break out if positive
                            if (result)
                                break;
                                
                        }
                    }
                }

                //Break out if positive
                if (result)
                    break;
            }

            return result;
        }
    }
}
