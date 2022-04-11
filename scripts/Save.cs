using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Sprite m_save_success;//存档成功动画
    SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BeShoot(int k)
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);//保存玩家位置
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

        PlayerPrefs.SetFloat("CameraPosX", Camera.main.transform.position.x);//保存摄像机位置
        PlayerPrefs.SetFloat("CameraPosY", Camera.main.transform.position.y);
        PlayerPrefs.SetFloat("CameraPosZ", Camera.main.transform.position.z);

        m_spriteRenderer.sprite = m_save_success;//动画变化（红色变成绿色的存档画面）;
    }
}
