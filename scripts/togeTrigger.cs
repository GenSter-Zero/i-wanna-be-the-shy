using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togeTrigger : MonoBehaviour
{
    //��̬�ܸ����˺�������
    public int m_damage = 30;
    void OnTriggerEnter2D(Collider2D other)//��������
    {
        if(other.gameObject.tag=="Player")//tag����
        {
            other.SendMessage("BeDamaged", m_damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
