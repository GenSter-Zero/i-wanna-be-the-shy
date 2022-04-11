using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public Rigidbody2D m_thorn;
    public float speed = 700f;
    public int MaxTurns = 3;
    private int turns = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            turns++;
            print("success");
            Vector2 v = new Vector2(speed, 0);
            m_thorn.velocity = v * Time.deltaTime;
            if(turns>=MaxTurns)
            {
                Destroy(gameObject);
            }
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
