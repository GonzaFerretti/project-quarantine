using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolNode))]
public class CustomEditorTest : Editor
{
    PatrolNode _myNode;
    List<ActionRotationWrapper> _myRotationWrappers;

    private void OnEnable()
    {
        _myNode = (PatrolNode)target;
        _myRotationWrappers = new List<ActionRotationWrapper>();
        for (int i = 0; i < _myNode.queuedAction.Length; i++)
        {
            if (_myNode.queuedAction[i] is ActionRotationWrapper)
            {
                _myRotationWrappers.Add(_myNode.queuedAction[i] as ActionRotationWrapper);
            }
        }

        for (int i = 0; i < _myRotationWrappers.Count; i++)
        {
            if (_myRotationWrappers[i].speed == 0)
            {
                _myRotationWrappers[i] = ScriptableObject.CreateInstance("ActionRotationWrapper") as ActionRotationWrapper;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        for (int i = 0; i < _myRotationWrappers.Count; i++)
        {
            EditorGUILayout.Space();
            _myRotationWrappers[i].speed = EditorGUILayout.FloatField("Rotation " + (i + 1).ToString() + " speed:", _myRotationWrappers[i].speed);
            _myRotationWrappers[i].maxDuration = EditorGUILayout.FloatField("Rotation " + (i + 1).ToString() + " duration:", _myRotationWrappers[i].maxDuration);
        }
    }
}
