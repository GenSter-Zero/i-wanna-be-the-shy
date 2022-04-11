using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public int m_damage = 10;
    void OnTriggerEnter2D(Collider2D other)//对人物伤害
    {
        if (other.gameObject.tag == "Player")//tag词条
        {
            other.SendMessage("BeDamaged", m_damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            Destroy(gameObject, 2.3f);//2.3s后自动删除
        }
    }
}
