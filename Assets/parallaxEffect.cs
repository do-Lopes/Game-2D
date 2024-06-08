using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPositionX, startPositionY;
    public float parallaxEffect;
    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = (Camera.main.transform.position.x * (1 - parallaxEffect));
        float dist = Camera.main.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPositionX + dist, startPositionY, transform.position.z);
        if (temp * 1.2f> startPositionX + length)        
            startPositionX += length;        
        else if (temp * 1.2 < startPositionX - length)
            startPositionX -= length;
    }
}