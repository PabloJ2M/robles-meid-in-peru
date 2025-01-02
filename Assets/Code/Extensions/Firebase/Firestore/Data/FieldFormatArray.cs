using System;
using System.Linq;
using System.Collections.Generic;

namespace Firebase.Firestore
{
    public interface IArrayField<T> : IField where T : IField
    {
        public T Add { set; }
        public bool Contains(T field);
    }

    [Serializable] public class ArrayContainer<T>
    {
        public List<T> values = new();

        public ArrayContainer() { }
        public ArrayContainer(int length) => values = new(new T[length]);
        public bool Contains(T item) => values.ToList().Contains(item);
    }
    [Serializable] public class IntArrayField : IArrayField<IntField>
    {
        public ArrayContainer<IntField> arrayValue = new();

        public object Value => arrayValue.values;
        public IntField Add { set => arrayValue.values.Add(value); }

        public IntArrayField() { }
        public IntArrayField(int length) => arrayValue.values = new(length);
        public IntArrayField(ArrayContainer<IntField> integerValue) => arrayValue = integerValue;
        public bool Contains(IntField field) => arrayValue.Contains(field);
        public bool Contains(int value) => arrayValue.values.Any(x => x.integerValue.Equals(value));
    }
    [Serializable] public class StringArrayField : IArrayField<StringField>
    {
        public ArrayContainer<StringField> arrayValue = new();

        public object Value => arrayValue.values;
        public StringField Add { set => arrayValue.values.Add(value); }

        public StringArrayField() { }
        public StringArrayField(int length) => arrayValue = new(length);
        public StringArrayField(ArrayContainer<StringField> integerValue) => arrayValue = integerValue;
        public bool Contains(StringField field) => arrayValue.Contains(field);
        public bool Contains(string value) => arrayValue.values.Any(x => x.stringValue.Replace("!", string.Empty).Equals(value));
        public int Find(string value) => arrayValue.values.FindIndex(x => x.stringValue.Equals(value));
    }
}