using System;
using System.Collections.Generic;

namespace Firebase.Firestore
{
    public interface IMapField<T> : IField where T : IField
    {
        public (string, T) Add { set; }
        public bool Contains(string key);
    }

    [Serializable] public class MapField<T>
    {
        public Dictionary<string, T> fields = new();

        public MapField() { }
        public MapField(Dictionary<string, T> fields) => this.fields = fields;
    }
    [Serializable] public class MapContainer<T>
    {
        public MapField<T> mapValue = new();

        public MapContainer() { }
        public MapContainer(MapField<T> mapValue) => this.mapValue = mapValue;
    }
    [Serializable] public class IntMap : IMapField<IntField>
    {
        public MapContainer<IntField> items  = new();

        public object Value => items.mapValue.fields;
        public (string, IntField) Add { set => items.mapValue.fields[value.Item1] = value.Item2; }

        public IntMap() { }
        public IntMap(MapContainer<IntField> items) => this.items = items;

        public bool Contains(string key) => items.mapValue.fields.ContainsKey(key);
        public bool Contains(string key, int value)
        {
            if (!Contains(key)) return false;
            else return items.mapValue.fields[key].integerValue == value;
        }
    }
    [Serializable] public class StringMap : IMapField<StringField>
    {
        public MapContainer<StringField> items = new();

        public object Value => items.mapValue.fields;
        public (string, StringField) Add { set => items.mapValue.fields[value.Item1] = value.Item2; }

        public StringMap() { }
        public StringMap(MapContainer<StringField> items) => this.items = items;

        public bool Contains(string key) => items.mapValue.fields.ContainsKey(key);
    }
}