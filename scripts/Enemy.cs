using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //boss����

    public GameObject m_player;//׷�����λ��
    public GameObject pfb_blood;//��Ч����λ��

    public int m_damage = 30;//�˺�Ѫ������Ϊ���Ѫ��Ϊ30������ֻҪboss�˺�Ҳ��30����ɱ��
    public int m_HP=1500;//bossѪ��

    private double time;//���ʱ��
    public double coldtime;//��ȴʱ��
    public GameObject runImage;//boss��Ļ

    public GameObject pfb_bullet;// �ӵ�����
    public Vector2 bulletSpeed = new Vector2(10, 0);
    // Start is called before the first frame update    
    void Start()
    {
        time = coldtime;//��ȴʱ�����
        EnemyHP.HealthMax = m_HP;//���Ѫ����
        EnemyHP.HealthCurrent = m_HP;//����Ѫ����
    }

    // Update is called once per frame
    void Update() 
    {
        if (m_HP <= 0)
        {
            Destroy(gameObject);//boss����������ֹͣ
            SoundManager.instance.levelMusic();
            runImage.SetActive(true);//���ִ��͵㣬������һ��
        }  
        if (m_HP > 1050 && m_HP <= 1500)
        {
            time = 0.25;
            if (coldtime > 0)
            {
                coldtime -= Time.deltaTime;
            }
            else
            {
                apple();
                coldtime = time;
            }
        }
        if (m_HP > 550 && m_HP <= 1000)
        {
            time = 0.25;
            if (coldtime > 0)
            {
                coldtime -= Time.deltaTime; //�ӵ�����ʱ��
            }
            else
            {
                stalk();
                coldtime = time;//���û���
            }
        }
        if (m_HP > 0 && m_HP <= 500)
        {
            time = 0.8;
            if (coldtime > 0)
            {
                coldtime -= Time.deltaTime;
            }
            if(coldtime < 0)
            {
                apple();
                appleleft();
                stalkUP();
                coldtime = time;
            }
        }
    }
    void BeShoot(int damage)//�����к��Ч��
    {
        Vector3 blood ;
        blood.x = Random.Range(transform.position.x- 0.5f, transform.position.x + 0.5f);
        blood.y = Random.Range(transform.position.y - 7f, transform.position.y + 7f);
        blood.z = transform.position.z;
        Instantiate(pfb_blood, blood, Quaternion.identity); //������Ч
        m_HP -= damage;//Ѫ������
        EnemyHP.HealthCurrent = m_HP;//Ѫ��������1
    }    

    void apple()//�Ҳ�apple�������
    { 
        bulletSpeed = new Vector2(10, 0);
        float y = Random.Range(-10.5f, 10.5f);
        Vector3 pos = new Vector3(11, y, transform.position.z);//���λ�õ�����Ԥ����
        GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = -1 * bulletSpeed;//�ٶȷ���
    }
    void appleleft()//���apple�������
    {
        bulletSpeed = new Vector2(10, 0);
        float y = Random.Range(-10.5f, 10.5f);
        Vector3 pos = new Vector3(-15, y, transform.position.z);//���λ�õ�����Ԥ����
        GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = 1 * bulletSpeed;//�ٶȷ���
    }
    void stalk()//�Ҳ�׷��apple
    {
        if(m_player != null)
        {
            GameObject obj = Instantiate(pfb_bullet, transform.position, Quaternion.identity);
            bulletSpeed = new Vector2(m_player.transform.position.x - obj.transform.position.x, m_player.transform.position.y - obj.transform.position.y);
            obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed * 0.7f;
        }
    }
    void stalkUP()//�ϲ�׷��apple
    {
        if (m_player != null)
        {
            float x = Random.Range(-11, 11);
            Vector3 pos = new Vector3(x, 10, transform.position.z);//���λ�õ�����Ԥ����
            GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
            bulletSpeed = new Vector2(m_player.transform.position.x - obj.transform.position.x, m_player.transform.position.y - obj.transform.position.y);
            obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed * 0.5f;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)//boss�����˺�
    {
        if (other.gameObject.tag == "Player")//tag����
        {
            other.SendMessage("BeDamaged", m_damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
