using Charon.Core.Entities;
using Charon.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;
using System.Reflection.Emit;
using System.Linq.Expressions;
using Charon.Core.Utilities;
using System.Collections;

namespace Charon.Messaging
{
    public class ListConverter
    {
        private List<object> _conversionItems = null;
        private List<object> _convertedItems = null;
        private List<object> _includeProperties = null;
        private List<string> _errors = null;
        private List<FieldNavigationList> _parsedIncludeFields = null;
        private Dictionary<Tuple<Type,string>, object> _encounteredObjects  = null;
        private Dictionary<Type, TypeAccessor> _cachedTypeAccessors = null;


        public List<object> IncludeProperties
        {
            get { return _includeProperties;  }
            set { _includeProperties = value; }
        }

        public List<string> Errors
        {
            get { return _errors;  }
            set { _errors = value; }
        }

        public List<object> ConvertedItems
        {
            get { return _convertedItems; }
        }

        public void ConvertToDTO(object[] entities, object[] includeProperties, List<string> errors)
        {
            _conversionItems = entities.ToList();
            _convertedItems = new List<object>();
            _includeProperties = new List<object>();
            _parsedIncludeFields = new List<FieldNavigationList>();
            _encounteredObjects = new Dictionary<Tuple<Type, string>, object>();
            _cachedTypeAccessors = new Dictionary<Type, TypeAccessor>();
            _errors = errors;

            if (entities == null || entities.Length == 0)
                return;

            if (includeProperties != null)
            {
                _includeProperties = includeProperties.ToList();
                ParseIncludeStrings();
            }

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
                    //If it matches a field on our included list then process manually
                    List<FieldNavigationList> fieldNavList = _parsedIncludeFields.Where(x => x.Fields.First() == member.Name).ToList();
                    if (fieldNavList != null)
                    {
                        accessor[convertedEntity, member.Name] = ProcessField(existingPropValue, fieldNavList);
                    }
                    //Otherwise set it to null
                    else
                    {
                        accessor[convertedEntity, member.Name] = null;
                    }
                }
                else
                {
                    //Its not an entitybase so just assign the object  to our new entity
                    accessor[convertedEntity, member.Name] = accessor[entity, member.Name];
                }
            }

            _convertedItems.Add(convertedEntity);
        }

        private object ProcessField(object entity, List<FieldNavigationList> fieldLists)
        {
            //Start with least specific so we don't overwrite things
            fieldLists = fieldLists.OrderBy(x => x.Fields.Count).ToList();

            //Get type accessor for root object/collection
            var entityType = entity.GetType();
            var rootTypeAccessor = GetTypeAccessor(entity.GetType());
            var newEntity = ShallowClone(entity);

            //Loop through field lists
            foreach (var fieldList in fieldLists)
            {
                //Plan here is to build object structure from the deepest level up
                var fieldDepthCount = fieldList.Fields.Count();
                FieldNavigationList currentFieldList = fieldList;
                object parentObject = null;
                object childObject = null;

                switch (fieldDepthCount)
                {
                    case 1:
                        //Nothing to do here as newEntity is already a shallow clone of entity
                        break;
                    default:
                        for (int i = (fieldDepthCount - 1); i >= 0; i--)
                        {
                            //As we loop through we lop off the end field to go down through the graph
                            currentFieldList = new FieldNavigationList { Fields = fieldList.Fields.GetRange(0, i + 1) };

                            //Get object - use GetNavObject if we're not at the root
                            if (i > 0)
                            {
                                parentObject = GetNavObject(entity, currentFieldList);
                                parentObject = ShallowClone(parentObject);

                            }
                            //We're at root so use our new entity that we will return
                            else
                                parentObject = newEntity;

                            //If we have a child then attach it to parent
                            if (childObject != null)
                            {
                                var parentAccessor = GetTypeAccessor(parentObject.GetType());
                                parentAccessor[parentObject, fieldList.Fields[i + 1]] = childObject;
                            }

                            //Keep ref to add and add it to next one
                            childObject = parentObject;
                        }
                        break;
                }
            }

            return newEntity;
        }

        private object GetNavObject(object entity, FieldNavigationList fieldList)
        {
            var accessor = GetTypeAccessor(entity.GetType());
            var parent = entity;
            var fieldCount = fieldList.Fields.Count();
            object child = null;

            //Start at field 1 as 0 is the entity itself
            for(int i = 1; i < fieldCount; i++)
            {
                if (child != null)
                    parent = child;

                child = accessor[parent, fieldList.Fields[i]];

                if (child == null)
                    break;

                accessor = GetTypeAccessor(child.GetType());
            }

            return child;
        }

         private void ParseIncludeStrings()
        {
            foreach (var exp in _includeProperties)
            {
                var expStr = exp.ToString();
                var startIndex = expStr.IndexOf("=>") + 2;

                if (startIndex == -1)
                {
                    _errors.Add(string.Format("Could not parse expression {0}", expStr));
                    continue;
                }

                //TODO - Rationalise\remove duplicate includes
                var fieldsString = expStr.Substring(startIndex, expStr.Length - startIndex);
                var fields = fieldsString.Split('.');
                var fieldList = new List<string>();

                for (int i = 1; i < fields.Length; i++)
                {
                    fieldList.Add(fields[i]);
                }

                _parsedIncludeFields.Add(new FieldNavigationList { Fields =  fieldList});
            }
        }

        public T ShallowClone<T>(T input)
        {
            if (input == null)
                return default(T);

            var inputType = input.GetType();

            if (inputType.IsAssignableFromEntityBase())
            {
                return ShallowCloneObject(input);
            }
            else if (inputType.IsCollectionAssignableFromEntityBase())
            {
                return ShallowCloneCollection(input);
            }
            else
            {
                return default(T);
            }
        }

        public T ShallowCloneObject<T>(T input)
        {
            //Check for null first
            if (input == null)
                return default(T);

            //First check whether we've encountered this object before
            var typeKey = new Tuple<Type, string>(input.GetType(), (input as EntityBase).Key().ToString());
            object encountered;

            //If we have then just return that one
            if (_encounteredObjects.TryGetValue(typeKey, out encountered))
                return (T)encountered;

            //Otherwise shallow clone it
            var accessor = GetTypeAccessor(input.GetType());
            var memberSet = accessor.GetMembers();
            var newOne = accessor.CreateNew();

            foreach(var member in memberSet)
            {
                if (member.Type.IsAssignableFromEntityBase() || member.Type.IsCollectionAssignableFromEntityBase())
                {
                    accessor[newOne, member.Name] = null;
                }
                else
                {
                    accessor[newOne, member.Name] = accessor[input, member.Name];
                }
            }

            //Add it to encountered and return it
            _encounteredObjects.Add(typeKey, newOne);
            return (T)newOne;
        }

        public T ShallowCloneCollection<T>(T input)
        {
            Type collectionType = input.GetType();
            var accessor = GetTypeAccessor(collectionType);
            IList cloneCollection = (IList)accessor.CreateNew();
            IList inputCollection = (IList)input;
            Type entityType = collectionType.GetGenericArguments().Single();
            accessor = GetTypeAccessor(entityType);

            foreach (var item in inputCollection)
            {
                var clone = ShallowClone(item);
                cloneCollection.Add(clone);
            }

            return (T)cloneCollection;
        }
    }

    internal class FieldNavigationList
    {
        public List<string> Fields { get; set; }
    }
}
