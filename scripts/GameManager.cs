using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //游戏全局管理

    public SpriteRenderer wherePlayer;//每个场景玩家应该出现的位置
    public GameObject m_player;//玩家位置，便于存档点的存储与读取
    // Start is called before the first frame update

    void Start()
    {
        Vector3 vec=Vector3.zero;

        vec.x = PlayerPrefs.GetFloat("CameraPosX", -0.5f);
        vec.y = PlayerPrefs.GetFloat("CameraPosY", 0.5f);
        vec.z = PlayerPrefs.GetFloat("CameraPosZ", -10);
        Camera.main.transform.position = vec;//固定摄像机位置

        vec.x = PlayerPrefs.GetFloat("PlayerPosX", wherePlayer.transform.position.x);
        vec.y = PlayerPrefs.GetFloat("PlayerPosY", wherePlayer.transform.position.y);
        vec.z = PlayerPrefs.GetFloat("PlayerPosZ", 0);
        m_player.transform.position = vec;//固定每个场景开始时出现位置
    }

        // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("return"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载画面，也就是游戏重开
            if(m_player == null)//按下后停下死亡音乐，然后继续开始播放刚刚的背景音乐
            {
                SoundManager.instance.stopDeathAudio();
                SoundManager.instance.StartLevelAudio();
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();//删除所有存档点数据，代表在该场景从头开始
        }


    }
}
