using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Firebase.Firestore.Extra
{
    public class LeaderboardEntry : UI_Entry<FirestoreDocument>
    {
        [SerializeField] private TextMeshProUGUI _nameUI;
        [SerializeField] private TextMeshProUGUI _scoreUI;

        public override void Setup(FirestoreDocument data)
        {
            if (!data.fields.ContainsKey("name")) { Pool.Release(this); return; }

            _nameUI?.SetText(data.fields["name"].Value.ToString());
            _scoreUI?.SetText(data.fields["highscore"].Value.ToString());
        }
    }
}