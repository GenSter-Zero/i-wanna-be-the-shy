using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //����ӵ�����
    public int bullet_damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)//�ӵ��˺�
    {
        //if(collision.gameObject.layer)
        collision.SendMessage("BeShoot", bullet_damage, SendMessageOptions.DontRequireReceiver);//�����������ֵ���Ƿ���Ҫ������
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
