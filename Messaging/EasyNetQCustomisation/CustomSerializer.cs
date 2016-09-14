using System.Text;
using Newtonsoft.Json;
using EasyNetQ;
using System.Collections;
using System.Collections.Generic;
using Charon.Core.Entities;
using System.Linq;
using System.IO;
using System;
using Charon.Core.Messaging;
using FastMember;

namespace Charon.Messaging.EasyNetQCustomisation
{
    internal class CustomSerializer : ISerializer
    {
        private readonly ITypeNameSerializer typeNameSerializer;

        private List<JsonConverter> GetConverters()
        {
            var converters = new List<JsonConverter>();

            foreach(var entityType in EntityTypes.EntityBaseTypes)
            {
                Type converterWithEntityType = typeof(ExpressionConverter<>).MakeGenericType(entityType);
                var accessor = TypeAccessor.Create(converterWithEntityType);
                var converter = accessor.CreateNew();
                converters.Add((JsonConverter)converter);
            }

            return converters;
        }

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            Formatting = Formatting.Indented,
        };

        public CustomSerializer(ITypeNameSerializer typeNameSerializer)
        {
            this.typeNameSerializer = typeNameSerializer;
            serializerSettings.Converters = serializerSettings.Converters.Concat(GetConverters()).ToList();
        }

        public byte[] MessageToBytes<T>(T message) where T : class
        {
            string stringVal = JsonConvert.SerializeObject(message, serializerSettings);
            byte[] bytes = Encoding.UTF8.GetBytes(stringVal);
            return bytes;
        }

        public T BytesToMessage<T>(byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), serializerSettings);
        }

        public object BytesToMessage(string typeName, byte[] bytes)
        {
            var type = typeNameSerializer.DeSerialize(typeName);
            var result = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(bytes), type, serializerSettings);
            return result;
        }
    }
}