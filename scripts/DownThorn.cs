using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownThorn : MonoBehaviour
{
    public Rigidbody2D m_thorn;
    public float speed = 1400f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //地图刺的向右移动
        if (other.tag == "Player")
        {
            print("success");
            Vector2 v = new Vector2(0, -speed);
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
