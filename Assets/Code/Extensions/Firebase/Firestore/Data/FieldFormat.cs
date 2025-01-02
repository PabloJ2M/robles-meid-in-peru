using System;
using Newtonsoft.Json;

namespace Firebase.Firestore
{
    [JsonConverter(typeof(FieldConverter))]
    public interface IField
    {
        public object Value { get; }
    }
    
    [Serializable] public class IntField : IField
    {
        public int integerValue;
        public object Value => integerValue;
        
        public IntField() { }
        public IntField(int integerValue) => this.integerValue = integerValue;
    }
    [Serializable] public class StringField : IField
    {
        public string stringValue;
        public object Value => stringValue;

        public StringField() { }
        public StringField(string stringValue) => this.stringValue = stringValue;
    }
}