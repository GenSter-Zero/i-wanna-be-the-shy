using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    //boss�ؿ�������ť���õĴ浵��ͼ��������boss
    public Sprite m_save_success;//����ɹ�����
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
        bossImage.SetActive(true);//����boss
        m_spriteRenderer.sprite = m_save_success;
        SoundManager.instance.bossMusic();//BGM����
        Destroy(gameObject);//����Ӱ����Ҵ�boss
    }
}
