using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //全局音乐

    public static SoundManager instance;//提前加载好，以便外界直接调用函数
    public AudioSource AudioSource;//音效
    public AudioSource backaudio;//背景音乐
    [SerializeField]
    private AudioClip fjumpAudio, sjumpAudio, deathAudio, backAudio,bossAudio;//私有数据但能被unity赋值
    // Update is called once per frame

    private void Awake()
    {
        if (instance!=null && instance != this)
        {
            Destroy(this.gameObject);//避免多个不可删除的该组件出现，也就是单个个例
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);//不允许删除，因为我需要保持音乐进度，切换场景，重新开始游戏都需要保存音乐
        backaudio = gameObject.AddComponent<AudioSource>();//获取组件
        AudioSource = gameObject.AddComponent<AudioSource>();
        backaudio.clip = backAudio;
        /*GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }*/
        instance.AudioSource.volume = 0.7f;//音量大小   
        instance.backaudio.volume = 0.5f;
        instance.backaudio.loop = true;//背景音乐循环播放
        StartLevelAudio();//开始时自动播放音乐
    }
    public void bossMusic()//到达boss场景后变化音乐
    {
        instance.backaudio.Stop();
        backaudio.clip = bossAudio;
        instance.backaudio.Play();
    }
    public void levelMusic()//普通背景的音乐
    {
        instance.backaudio.Stop();
        backaudio.clip = backAudio;
        instance.backaudio.Play();
    }
    public void fJumpAudio()//第一次跳跃音效
    {
        AudioSource.clip = fjumpAudio;
        AudioSource.Play();
    }
    public void sJumpAudio()//第二次跳跃音效
    {
        AudioSource.clip = sjumpAudio;
        AudioSource.Play();
    }
    public void DeathAudio()//死亡音效
    {
        AudioSource.clip = deathAudio;
        AudioSource.Play();
    }
    public void stopDeathAudio()//停止死亡音乐
    {
        AudioSource.clip = deathAudio;
        AudioSource.Stop();
    }
    public void StartLevelAudio()//开始背景音乐
    {
        instance.backaudio.Play();
    }
    public void pauseLevelAudio()//暂停背景音乐
    {
        instance.backaudio.Pause();
    }
}
