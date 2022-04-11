using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togeTrigger : MonoBehaviour
{
    //静态能给予伤害的物体
    public int m_damage = 30;
    void OnTriggerEnter2D(Collider2D other)//人物死亡
    {
        if(other.gameObject.tag=="Player")//tag词条
        {
            other.SendMessage("BeDamaged", m_damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
