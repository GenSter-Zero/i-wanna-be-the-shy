using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //boss代码

    public GameObject m_player;//追踪玩家位置
    public GameObject pfb_blood;//特效产生位置

    public int m_damage = 30;//伤害血量，因为玩家血量为30，所以只要boss伤害也是30就秒杀了
    public int m_HP=1500;//boss血量

    private double time;//间隔时间
    public double coldtime;//冷却时间
    public GameObject runImage;//boss画幕

    public GameObject pfb_bullet;// 子弹构造
    public Vector2 bulletSpeed = new Vector2(10, 0);
    // Start is called before the first frame update    
    void Start()
    {
        time = coldtime;//冷却时间给予
        EnemyHP.HealthMax = m_HP;//最大血量条
        EnemyHP.HealthCurrent = m_HP;//现在血量条
    }

    // Update is called once per frame
    void Update() 
    {
        if (m_HP <= 0)
        {
            Destroy(gameObject);//boss死亡，音乐停止
            SoundManager.instance.levelMusic();
            runImage.SetActive(true);//出现传送点，进入下一关
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
                coldtime -= Time.deltaTime; //子弹缓冲时间
            }
            else
            {
                stalk();
                coldtime = time;//重置缓冲
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
    void BeShoot(int damage)//被射中后的效果
    {
        Vector3 blood ;
        blood.x = Random.Range(transform.position.x- 0.5f, transform.position.x + 0.5f);
        blood.y = Random.Range(transform.position.y - 7f, transform.position.y + 7f);
        blood.z = transform.position.z;
        Instantiate(pfb_blood, blood, Quaternion.identity); //生成特效
        m_HP -= damage;//血量减少
        EnemyHP.HealthCurrent = m_HP;//血量条更新1
    }    

    void apple()//右侧apple随机出现
    { 
        bulletSpeed = new Vector2(10, 0);
        float y = Random.Range(-10.5f, 10.5f);
        Vector3 pos = new Vector3(11, y, transform.position.z);//随机位置的生成预制体
        GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = -1 * bulletSpeed;//速度方向
    }
    void appleleft()//左侧apple随机出现
    {
        bulletSpeed = new Vector2(10, 0);
        float y = Random.Range(-10.5f, 10.5f);
        Vector3 pos = new Vector3(-15, y, transform.position.z);//随机位置的生成预制体
        GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().velocity = 1 * bulletSpeed;//速度方向
    }
    void stalk()//右侧追踪apple
    {
        if(m_player != null)
        {
            GameObject obj = Instantiate(pfb_bullet, transform.position, Quaternion.identity);
            bulletSpeed = new Vector2(m_player.transform.position.x - obj.transform.position.x, m_player.transform.position.y - obj.transform.position.y);
            obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed * 0.7f;
        }
    }
    void stalkUP()//上侧追踪apple
    {
        if (m_player != null)
        {
            float x = Random.Range(-11, 11);
            Vector3 pos = new Vector3(x, 10, transform.position.z);//随机位置的生成预制体
            GameObject obj = Instantiate(pfb_bullet, pos, Quaternion.identity);
            bulletSpeed = new Vector2(m_player.transform.position.x - obj.transform.position.x, m_player.transform.position.y - obj.transform.position.y);
            obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed * 0.5f;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)//boss本体伤害
    {
        if (other.gameObject.tag == "Player")//tag词条
        {
            other.SendMessage("BeDamaged", m_damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
