using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Firebase.Firestore
{
    public class FieldConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(IField).IsAssignableFrom(objectType);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject field = new();

            //single field format
            if (value is IntField intField) field["integerValue"] = intField.integerValue;
            if (value is StringField stringField) field["stringValue"] = stringField.stringValue;

            //array field format
            if (value is IntArrayField intArrayField)
            {
                JArray array = new();
                foreach (var item in intArrayField.arrayValue.values) array.Add(new JObject() { ["integerValue"] = item.integerValue });
                field["arrayValue"] = new JObject { ["values"] = array };
            }
            if (value is StringArrayField stringArrayField)
            {
                JArray array = new();
                foreach (var item in stringArrayField.arrayValue.values) array.Add(new JObject { ["stringValue"] = item.stringValue });
                field["arrayValue"] = new JObject { ["values"] = array };
            }

            //dictionary field format
            if (value is IntMap intMap)
            {
                JObject map = new(); JObject mapValue = new();
                foreach (var kvp in intMap.items.mapValue.fields) map[kvp.Key] = new JObject { ["integerValue"] = kvp.Value.integerValue };
                mapValue["fields"] = map; field["mapValue"] = mapValue;
            }
            if (value is StringMap stringMap)
            {
                JObject map = new(); JObject mapValue = new();
                foreach (var kvp in stringMap.items.mapValue.fields) map[kvp.Key] = new JObject { ["stringValue"] = kvp.Value.stringValue };
                mapValue["fields"] = map; field["mapValue"] = mapValue;
            }

            field.WriteTo(writer);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject field = JObject.Load(reader);

            try
            {
                //single field format
                if (field["integerValue"] != null) return ObjectToField(field, serializer, new IntField());
                if (field["stringValue"] != null) return ObjectToField(field, serializer, new StringField());

                //array field format
                if (field["arrayValue"] != null)
                {
                    if (field["arrayValue"]["values"] != null && field["arrayValue"]["values"].Type == JTokenType.Array)
                    {
                        var firstElement = field["arrayValue"]["values"].First;
                        if (firstElement["integerValue"] != null) return ObjectToField(field, serializer, new IntArrayField());
                        if (firstElement["stringValue"] != null) return ObjectToField(field, serializer, new StringArrayField());
                    }
                }

                //dictionary field format
                if (field["mapValue"] != null)
                {
                    if (field["mapValue"].Type == JTokenType.Object)
                    {
                        var mapElement = field["mapValue"]["fields"].First as JProperty;
                        if (mapElement != null)
                        {
                            if (mapElement.Value["integerValue"] != null) return ObjectToDictionary<IntMap, IntField>(field, new());
                            if (mapElement.Value["stringValue"] != null) return ObjectToDictionary<StringMap, StringField>(field, new());
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IField ObjectToField<T>(JObject obj, JsonSerializer s, T field) where T : IField
        {
            s.Populate(obj.CreateReader(), field);
            return field;
        }
        private IField ObjectToDictionary<T, F>(JObject obj, T field) where T : IMapField<F> where F : IField
        {
            foreach (var kvp in obj["mapValue"]["fields"])
            {
                var typeField = kvp.First.ToObject<F>();
                field.Add = (kvp.Path.Split('.').Last(), typeField);
            }   
            return field as IField;
        }
    }
}