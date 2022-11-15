using UnityEditor;
using UnityEngine;

namespace SubEffects
{
	[CustomEditor(typeof(GhostSprites))]
	[CanEditMultipleObjects]
	public class GhostSpritesCustomEditor : UnityEditor.Editor {
	
		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector();

			if (!GUILayout.Button("Restore Defaults")) return;
			
			GhostSprites sprites = (GhostSprites) target;
			sprites.RestoreDefaults();
		}
	}
}
