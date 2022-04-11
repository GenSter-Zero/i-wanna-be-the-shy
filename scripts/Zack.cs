using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zack : MonoBehaviour
{
    //zackµÄÒÆ¶¯¹ì¼£
    public GameObject zackImage; 
    public Rigidbody2D m_thorn;
    public float speed = 300f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("success");
            zackImage.SetActive(true);
            Vector2 v = new Vector2(0, speed);
            m_thorn.velocity = v * Time.deltaTime;
            Destroy(gameObject);
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
