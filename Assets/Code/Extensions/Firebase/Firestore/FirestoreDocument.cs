using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Firebase.Firestore
{
    [Serializable] public class FirestoreSnapshot
    {
        public List<FirestoreDocument> documents = new();

        public FirestoreSnapshot(string json)
        {
            JObject response = JObject.Parse(json);
            JArray documents = response["documents"] as JArray;
            foreach (var document in documents) this.documents.Add(new(document.ToString()));
        }
    }

    [Serializable] public class FirestoreDocument
    {
        public Dictionary<string, IField> fields = new();

        public FirestoreDocument() { }
        public FirestoreDocument(string documents)
        {
            var document = JObject.Parse(documents);
            fields = JsonConvert.DeserializeObject<Dictionary<string, IField>>(document["fields"].ToString());
        }

        public IField GetField(string id) => fields.ContainsKey(id) ? fields[id] : null;
        public void Update(string name, IField value) => fields[name] = value;
        public string Mask() => $"?updateMask.fieldPaths={string.Join("&updateMask.fieldPaths=", fields.Keys)}";
        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}