using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerinto : MonoBehaviour
{
    //��ͼ�仯�����뵽��һ�����Ļ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")//tag����
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
