using Environment.LocationChunk;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace Editor
{
    [EditorTool("Custom Snap Move", typeof(CustomSnap))]
    public class CustomSnappingTool : EditorTool
    {
        public Texture2D toolIcon;

        private Transform _oldTarget;
        private CustomSnapPoint[] _allPoints;
        private CustomSnapPoint[] _targetPoints;
        
        public override GUIContent toolbarIcon
        {
            get
            {
                return new GUIContent
                {
                    image = toolIcon,
                    text = "Custom Snap Move Tool",
                };
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            var targetTransform = ((CustomSnap) target).transform;

            if (targetTransform != _oldTarget)
            {
                PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

                _allPoints = prefabStage != null 
                    ? prefabStage.prefabContentsRoot.GetComponentsInChildren<CustomSnapPoint>() 
                    : FindObjectsOfType<CustomSnapPoint>();
            
                _targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

                _oldTarget = targetTransform;
            }

            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.Euler(292.987183f,351.956573f,8.72267342f));

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(targetTransform, "Move with custom snap tool");
            
                MoveWithSnapping(targetTransform, newPosition);
            }
        }

        private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
        {
            var bestPosition = newPosition;
            var closestDistance = float.PositiveInfinity;

            foreach (var point in _allPoints)
            {
                if (point.transform.parent == targetTransform) continue;

                foreach (var ownPoint in _targetPoints)
                {
                    var targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                    var distance = Vector3.Distance(targetPos, newPosition);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestPosition = targetPos;
                    }
                }
            }

            targetTransform.position = closestDistance < 10f ? bestPosition : newPosition;
        }
    }
}
