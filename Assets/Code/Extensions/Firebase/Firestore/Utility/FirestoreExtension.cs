namespace Firebase.Firestore
{
    public static class FirestoreExtension
    {
        public static void SetDocument(this FirestoreDocument document, params object[] args)
        {
            foreach (var item in args)
            {
                if (item is Data<IntField> intF) { SetField(ref document, intF); continue; }
                if (item is Data<StringField> stringF) { SetField(ref document, stringF); continue; }
                if (item is Data<IntArrayField> intArray) { SetField(ref document, intArray); continue; }
                if (item is Data<IntMap> intMap) { SetField(ref document, intMap); continue; }
            }
        }
        private static void SetField<T>(ref FirestoreDocument document, Data<T> value) where T : IField
        {
            if (!value.HasChanged) return;
            document.Update(value.SaveID, value.Items);
        }

        public static void GetDocument(this FirestoreDocument document, params object[] args)
        {
            foreach (var item in args)
            {
                if (item is Data<IntField> intF) { GetField(ref document, intF); continue; }
                if (item is Data<StringField> stringF) { GetField(ref document, stringF); continue; }
                if (item is Data<IntArrayField> intArray) { GetField(ref document, intArray); continue; }
                if (item is Data<IntMap> intMap) { GetField(ref document, intMap); continue; }
            }
        }
        private static void GetField<T>(ref FirestoreDocument document, Data<T> value) where T : IField
        {
            IField field = document.GetField(value.SaveID);
            if (field != null) value.ReplaceData((T)field);
        }
    }
}