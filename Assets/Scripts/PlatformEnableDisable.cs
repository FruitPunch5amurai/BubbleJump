using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnableDisable : MonoBehaviour
{

    BoxCollider2D boxCollider2D;
    Transform OnSwitch;
    Transform OffSwitch;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        OnSwitch = LevelManager.Instance.player.GetComponent<Transform>().GetChild(0);
        OffSwitch = LevelManager.Instance.player.GetComponent<Transform>().GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {

        if(LevelManager.Instance.player.GetComponent<Rigidbody2D>().velocity.y < 0.0f && LevelManager.Instance.player.transform.position.y > transform.position.y)
        {
            boxCollider2D.enabled = true;
        }
        else
        {
            boxCollider2D.enabled = false;
        }
        //Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + boxCollider2D.size.y + playerTransform.GetComponent<BoxCollider2D>().size.y * 3.5f), Color.red);
        //Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + boxCollider2D.size.y + playerTransform.GetComponent<BoxCollider2D>().size.y), Color.red);

        //if (OnSwitch.position.y > transform.position.y)
        //{
        //    boxCollider2D.enabled = true;
        //}
        //else if(OffSwitch.position.y < transform.position.y)
        //{
        //    boxCollider2D.enabled = false;
        //}
    }
}
