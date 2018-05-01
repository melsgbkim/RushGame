﻿using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyCollisionHandler>().CollisionFromPlayer(gameObject);
            collision.gameObject.GetComponent<EnemyInfo>().Player = gameObject;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("DropItem"))
        {
            ItemInfo info = collision.gameObject.GetComponent<ItemInfo>();
            if (info.CanGetItem)
            {
                info.ItemGetByPlayer(gameObject.transform);
            }
        }*/
    }
}
