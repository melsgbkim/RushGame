using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : MonoBehaviour
{
    public void CollisionFromPlayer(GameObject player)
    {
        GetComponent<EnemyLogic>().Damaged(0);
    }
}
