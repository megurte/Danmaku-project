using System.Collections;
using UnityEngine;

namespace Boss.Kirin
{
    public class KirinMove : MonoBehaviour
    {
        public float speed;
        public bool isMoving = false;
        public Vector3 toPosition;

        private void Update()
        {
            if (isMoving)
                MovementToPosition(toPosition);

            if (transform.position == toPosition)
                isMoving = false;
        }

        private void MovementToPosition(Vector3 targetPos)
        {    
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);   
        }

        public IEnumerator MoveTo(KirinMoveSettings settings)
        {
            yield return new WaitForSeconds(settings.waitTime);
        
            isMoving = true;
            toPosition = settings.position;
        }
    }
}
