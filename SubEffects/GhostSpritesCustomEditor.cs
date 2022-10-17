using UnityEditor;
using UnityEngine;

namespace SubEffects
{
	[CustomEditor(typeof(GhostSprites))]
	[CanEditMultipleObjects]
	public class GhostSpritesCustomEditor : Editor {
	
		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector();

			if(GUILayout.Button ("Restore Defaults")){
				GhostSprites sprites = (GhostSprites) target;
				sprites.RestoreDefaults();
			}

		}
	}
}
