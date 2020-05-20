using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float platformSizeX = ((GetComponent<BoxCollider2D>().size.x/2) * transform.localScale.x);
        Vector2 left = new Vector2(transform.position.x - platformSizeX, transform.position.y);
        Vector2 right = new Vector2(transform.position.x + platformSizeX, transform.position.y);

        Debug.DrawLine(left, right,Color.red);
    }
}
