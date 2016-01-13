using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(TechnologyButton))]
public class UISegmentedControlButtonEditor : UnityEditor.UI.ButtonEditor {

	public override void OnInspectorGUI() {
		// Set editable fields
		TechnologyButton component = (TechnologyButton)target;
		component.associatedTechnology  = (Technology)EditorGUILayout.ObjectField("Associated Technology", 
		                                                                          component.associatedTechnology, 
																				  typeof(Technology), true);
		
		component.technologyImage  = (Image)EditorGUILayout.ObjectField("Technology Image", 
																			   	  component.technologyImage, 
																				  typeof(Image), true);
		
		// Then, set the Technology Image as the central image of this button
		component.technologyImage.sprite = component.associatedTechnology.technologySprite;
		base.OnInspectorGUI();
	}
}
