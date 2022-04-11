using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpThorn : MonoBehaviour
{
    public Rigidbody2D m_thorn;
    public float speed = 1400f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //刺的向上移动
        if (other.tag == "Player")
        {
            print("success");
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
