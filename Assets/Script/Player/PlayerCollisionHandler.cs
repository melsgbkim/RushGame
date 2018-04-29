using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyCollisionHandler>().CollisionFromPlayer(gameObject);
        }
    }
}
