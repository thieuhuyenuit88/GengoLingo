using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScrollRectNested))]
[CanEditMultipleObjects]
public class ScrollRectNestedEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
