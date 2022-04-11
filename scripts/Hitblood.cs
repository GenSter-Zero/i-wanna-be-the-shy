using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitblood : MonoBehaviour
{
    //特效删除时间
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
