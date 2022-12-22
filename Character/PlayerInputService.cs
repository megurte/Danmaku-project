using UnityEngine;

namespace Character
{
    public class PlayerInputService : MonoBehaviour
    {
        public float GetHorizontalAxisValue()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetVerticalAxisValue()
        {
            return Input.GetAxis("Vertical");
        }
        
        public bool IsSlowKeyDown()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
        
        public bool IsSlowKeyUp()
        {
            return Input.GetKeyUp(KeyCode.LeftShift);
        }

        public bool IsMovementControlKeysDown()
        {
            return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ||
                   Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        }
        
        public bool IsShootKeyPressed()
        {
            return Input.GetKey(KeyCode.Z);
        }
        
        public bool IsSpecialKeyPressed()
        {
            return Input.GetKey(KeyCode.X);
        }
        
        public bool IsReturnKeyPressed()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}