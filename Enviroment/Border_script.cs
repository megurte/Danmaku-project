using UnityEngine;

public class Border_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(collision.gameObject);
    }
}
