using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenShoot))]
public class EditorScreenShoot : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScreenShoot capture = (ScreenShoot)target;

        if(GUILayout.Button("Take Screen Shoot"))
        {
            capture.TakeScreenShoot();
        }
    }
}