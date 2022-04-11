using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //��Ϸȫ�ֹ���

    public SpriteRenderer wherePlayer;//ÿ���������Ӧ�ó��ֵ�λ��
    public GameObject m_player;//���λ�ã����ڴ浵��Ĵ洢���ȡ
    // Start is called before the first frame update

    void Start()
    {
        Vector3 vec=Vector3.zero;

        vec.x = PlayerPrefs.GetFloat("CameraPosX", -0.5f);
        vec.y = PlayerPrefs.GetFloat("CameraPosY", 0.5f);
        vec.z = PlayerPrefs.GetFloat("CameraPosZ", -10);
        Camera.main.transform.position = vec;//�̶������λ��

        vec.x = PlayerPrefs.GetFloat("PlayerPosX", wherePlayer.transform.position.x);
        vec.y = PlayerPrefs.GetFloat("PlayerPosY", wherePlayer.transform.position.y);
        vec.z = PlayerPrefs.GetFloat("PlayerPosZ", 0);
        m_player.transform.position = vec;//�̶�ÿ��������ʼʱ����λ��
    }

        // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("return"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//���¼��ػ��棬Ҳ������Ϸ�ؿ�
            if(m_player == null)//���º�ͣ���������֣�Ȼ�������ʼ���Ÿոյı�������
            {
                SoundManager.instance.stopDeathAudio();
                SoundManager.instance.StartLevelAudio();
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();//ɾ�����д浵�����ݣ������ڸó�����ͷ��ʼ
        }


    }
}
