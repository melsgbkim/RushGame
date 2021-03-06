﻿using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
    bool dead = false;
    Rigidbody2D rigid2D;
    // Use this for initialization
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void Damaged(float damage)
    {
        dead = true;
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            gameObject.GetComponent<EnemyInfo>().OnDead();
            MainEffManager.Get.NewEff("Eff_EnemyDead", transform.localPosition);
            Destroy(gameObject);
        }
    }
}
