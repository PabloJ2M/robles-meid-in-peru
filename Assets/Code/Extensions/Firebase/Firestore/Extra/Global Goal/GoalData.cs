namespace Firebase.Firestore.Extra
{
    public class GoalData
    {
        public readonly string file = "score";
        public int max, current;

        public GoalData(FirestoreDocument document)
        {
            current = (int)document.fields[file].Value;
            max = (int)document.fields["max"].Value;
        }
    }
}