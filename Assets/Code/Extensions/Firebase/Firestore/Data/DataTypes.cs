using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Firebase.Firestore
{
    public abstract class Data<IField>
    {
        public abstract IField Items { get; protected set; }
        public abstract string SaveID { get; }
        public bool HasChanged { get; protected set; }

        public Action<IField> OnItemsUpdated;
        protected void OnUpdate() => OnItemsUpdated?.Invoke(Items);
        public void ReplaceData(IField data) { Items = data; OnUpdate(); }

        public virtual void Save() => PlayerPrefs.SetString(SaveID, JsonUtility.ToJson(Items));
        public virtual void Load()
        {
            string json = PlayerPrefs.GetString(SaveID, string.Empty);
            if (!string.IsNullOrEmpty(json)) Items = JsonUtility.FromJson<IField>(json);
            OnUpdate();
        }
    }
    public abstract class ArrayData<Field, T> : Data<Field> where Field : IArrayField<T> where T : IField
    {
        protected bool IsItemOnList(object id)
        {
            if (id is int index) return (Items as IntArrayField).Contains(index);
            if (id is string key) return (Items as StringArrayField).Contains(key);
            return false;
        }
    }
    public abstract class MapData<Field, T> : Data<Field> where Field : IMapField<T> where T : IField
    {
        public bool IsItemOnList(string id) => Items.Contains(id);
        public override void Save()
        {
            string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
            PlayerPrefs.SetString(SaveID, json);
        }
        public override void Load()
        {
            var result = Items;
            string json = PlayerPrefs.GetString(SaveID, string.Empty);
            if (!string.IsNullOrEmpty(json)) result = JsonConvert.DeserializeObject<Field>(json);
            if (result != null) Items = result;
            OnUpdate();
        }
    }
}