using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Rigidbody2D m_thorn;
    public float speed = 1400f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            print("success");
            Vector2 v = new Vector2(0, speed);
            m_thorn.velocity = v * Time.deltaTime;
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }
    void Update()
    {

    }
}
