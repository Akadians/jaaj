using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float parallaxEffect;
    public Transform cam;

    private float length;
    private float startPos;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;                 
    }
    void Update()
    {
        float Repos = cam.transform.position.x * (1 - parallaxEffect);
        float Distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + Distance, transform.position.y, transform.position.z);

        if (Repos > startPos + length)
        {
            startPos += length;
        }
        else if (Repos < startPos - length)
        {
            startPos -= length;
        }
    }
}
