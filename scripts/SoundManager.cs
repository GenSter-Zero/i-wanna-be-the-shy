using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //ȫ������

    public static SoundManager instance;//��ǰ���غã��Ա����ֱ�ӵ��ú���
    public AudioSource AudioSource;//��Ч
    public AudioSource backaudio;//��������
    [SerializeField]
    private AudioClip fjumpAudio, sjumpAudio, deathAudio, backAudio,bossAudio;//˽�����ݵ��ܱ�unity��ֵ
    // Update is called once per frame

    private void Awake()
    {
        if (instance!=null && instance != this)
        {
            Destroy(this.gameObject);//����������ɾ���ĸ�������֣�Ҳ���ǵ�������
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);//������ɾ������Ϊ����Ҫ�������ֽ��ȣ��л����������¿�ʼ��Ϸ����Ҫ��������
        backaudio = gameObject.AddComponent<AudioSource>();//��ȡ���
        AudioSource = gameObject.AddComponent<AudioSource>();
        backaudio.clip = backAudio;
        /*GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }*/
        instance.AudioSource.volume = 0.7f;//������С   
        instance.backaudio.volume = 0.5f;
        instance.backaudio.loop = true;//��������ѭ������
        StartLevelAudio();//��ʼʱ�Զ���������
    }
    public void bossMusic()//����boss������仯����
    {
        instance.backaudio.Stop();
        backaudio.clip = bossAudio;
        instance.backaudio.Play();
    }
    public void levelMusic()//��ͨ����������
    {
        instance.backaudio.Stop();
        backaudio.clip = backAudio;
        instance.backaudio.Play();
    }
    public void fJumpAudio()//��һ����Ծ��Ч
    {
        AudioSource.clip = fjumpAudio;
        AudioSource.Play();
    }
    public void sJumpAudio()//�ڶ�����Ծ��Ч
    {
        AudioSource.clip = sjumpAudio;
        AudioSource.Play();
    }
    public void DeathAudio()//������Ч
    {
        AudioSource.clip = deathAudio;
        AudioSource.Play();
    }
    public void stopDeathAudio()//ֹͣ��������
    {
        AudioSource.clip = deathAudio;
        AudioSource.Stop();
    }
    public void StartLevelAudio()//��ʼ��������
    {
        instance.backaudio.Play();
    }
    public void pauseLevelAudio()//��ͣ��������
    {
        instance.backaudio.Pause();
    }
}
