using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //玩家子弹攻击
    public int bullet_damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)//子弹伤害
    {
        //if(collision.gameObject.layer)
        collision.SendMessage("BeShoot", bullet_damage, SendMessageOptions.DontRequireReceiver);//函数名，输出值，是否需要接收者
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
