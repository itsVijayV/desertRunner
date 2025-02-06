using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepeatingBackground : MonoBehaviour
{
    public GameObject Camera;
    public float parallaxEffect;
    private float width, postionX;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        postionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float parallaxDistance = Camera.transform.position.x * parallaxEffect;
        float remainingDistance = Camera.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(postionX + parallaxEffect, transform.position.y, transform.position.z);
        
        if (remainingDistance > postionX + width )
        {
            postionX += width;
        }
    }
}
