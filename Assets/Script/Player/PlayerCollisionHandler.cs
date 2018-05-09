using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour
{
/*    void Start()
    {
        Vector2 tmp1 = new Vector2(-9.8f, 0f);
        Vector2 tmp2 = new Vector2(-7.8f, 0f);
        Vector2 tmp3 = tmp1.normalized;
        Vector2 tmp4 = new Vector2(3f, 4f);
        Vector2 tmp5 = new Vector2(-9.8f, 0f);
        print(tmp1);
    }*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyCollisionHandler>().CollisionFromPlayer(gameObject);
            collision.gameObject.GetComponent<EnemyInfo>().Player = gameObject;
            PopupTextList.New(Random.Range(1, 800).ToString(),collision.transform.position);

            Rigidbody2D NowRigid = GetComponent<Rigidbody2D>();
            PlayerMove ComponentMove = GetComponent<PlayerMove>();
            PlayerStat ComponentStat = GetComponent<PlayerStat>();

            float MonsterHp = 1;
            float PowerLossAmount = MonsterHp / ComponentStat.Stat.mas;
            float NextSpeed = ComponentMove.BeforeVel.magnitude - PowerLossAmount;
            if (NextSpeed < 0) NextSpeed = 0f;
            NowRigid.velocity = NowRigid.velocity.normalized * NextSpeed;
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
