using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    //boss关卡开启按钮，用的存档点图标来开启boss
    public Sprite m_save_success;//射击成功动画
    SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        SoundManager.instance.pauseLevelAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject bossImage;
    void BeShoot(int k)
    {
        bossImage.SetActive(true);//出现boss
        m_spriteRenderer.sprite = m_save_success;
        SoundManager.instance.bossMusic();//BGM更换
        Destroy(gameObject);//避免影响玩家打boss
    }
}
