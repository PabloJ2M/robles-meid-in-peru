using UnityEngine;
using UnityEditor;

namespace Firebase.Auth
{
    [CustomEditor(typeof(AuthEditor))]
    public class AuthBehaviorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AuthEditor auth = target as AuthEditor;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Password");
            auth.password = EditorGUILayout.PasswordField(auth.password);
            EditorGUILayout.EndHorizontal();

            AuthEditor editor = target as AuthEditor;
            if (GUILayout.Button("Register")) editor.Register();
            if (GUILayout.Button("Sign In")) editor.SignIn();
            if (GUILayout.Button("Sign Out")) editor.SignOut();

            Undo.RecordObject(auth, "change password");
        }
    }
}