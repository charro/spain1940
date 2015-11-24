using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(TechnologyButton))]
public class UISegmentedControlButtonEditor : UnityEditor.UI.ButtonEditor {

	public override void OnInspectorGUI() {
		TechnologyButton component = (TechnologyButton)target;
		component.associatedTechnology  = (Technology)EditorGUILayout.ObjectField("Associated Technology", 
		                                                                          component.associatedTechnology, typeof(Technology), true);

		base.OnInspectorGUI();
	}
}
