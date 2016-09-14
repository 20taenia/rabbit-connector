using Charon.Core.Entities;
using Charon.Core.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Serialize.Linq.Extensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Text.RegularExpressions;
using Serialize.Linq.Nodes;

namespace Charon.Messaging.EasyNetQCustomisation
{
    public class ExpressionConverter<T> : JsonConverter
    {
        public override bool CanRead { get { return true; }}
        public override bool CanWrite { get { return true; }}

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                Type genericObjectType = objectType.GetGenericTypeDefinition();
                if (EntityTypes.TypesWithExpressions.Contains(genericObjectType))
                {
                    Type inputContainedType = objectType.GetGenericArguments()[0];
                    Type currentContainedType = this.GetType().GetGenericArguments()[0];

                    return (inputContainedType == currentContainedType);
                }
                else
                    return false;
            }
            else
                return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result = null;

            if (objectType.IsGenericType)
            {
                var genericType = objectType.GetGenericTypeDefinition();
                var expressionSerializer = new Serialize.Linq.Serializers.JsonSerializer();

                if (genericType == typeof(FilterHandle<>))
                {
                    JObject jObject = JObject.Load(reader);
                    var expressionJson = jObject.First.First.ToString();
                    var expressionNode = expressionSerializer.Deserialize<ExpressionNode>(expressionJson);
                    var expression = expressionNode.ToBooleanExpression<T>();
                    result = new FilterHandle<T>(expression);

                }
                else if (genericType == typeof(NavigationPropertiesHandle<>))
                {
                    JObject jObject = JObject.Load(reader);
                    var expressionObjects = jObject.First.First;
                    Expression<Func<T, object>>[] expressionArray = new Expression<Func<T, object>>[expressionObjects.Count()];
                    int index = 0;

                    foreach (var expressionToken in expressionObjects)
                    {
                        var expressionJson = expressionToken.First.First.ToString();
                        var expressionNode = expressionSerializer.Deserialize<ExpressionNode>(expressionJson);
                        var expression = expressionNode.ToExpression<Func<T, object>>();
                        expressionArray[index] = expression;
                        index++;
                    }

                    result = new NavigationPropertiesHandle<T>(expressionArray);

                }
            }

            return result; 
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type objectType = value.GetType();

            if (objectType.IsGenericType)
            {
                var genericType = objectType.GetGenericTypeDefinition();

                if (genericType == typeof(FilterHandle<>))
                {
                    var expression = (value as FilterHandle<T>).GetFilter();
                    writer.WriteStartObject();
                    writer.WritePropertyName("ExpressionNode");

                    if (expression == null)
                    {
                        writer.WriteEndObject();
                        return;
                    }

                    string expressionNode = expression.ToJson();
                    writer.WriteRawValue(expressionNode);
                    writer.WriteEndObject();
                }
                else if (genericType == typeof(NavigationPropertiesHandle<>))
                {
                    var expressions = (value as NavigationPropertiesHandle<T>).GetNavigationProperties();

                    writer.WriteStartObject();
                    writer.WritePropertyName("ExpressionNodes");

                    if (expressions == null)
                    {
                        writer.WriteEndObject();
                        return;
                    }

                    writer.WriteStartArray();

                    foreach (var expression in expressions)
                    {
                        string expressionNode = expression.ToJson();
                        writer.WriteStartObject();
                        writer.WritePropertyName("ExpressionNode");
                        writer.WriteRawValue(expressionNode);
                        writer.WriteEndObject();
                    }

                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }
            }
        }
    }
}

