using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private PlayerManager playerManager;
    private Vector3 screenBottomLeft;
    private Vector3 screenTopRight;

    private float rightLimit = 2.5f;
    private float leftLimit = 1.0f;
    public float speed = 2.0f;
    private int direction = 1;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = LevelManager.Instance.player.GetComponent<PlayerManager>();

        leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z)).x + GetComponent<BoxCollider2D>().size.x;
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z)).x - GetComponent<BoxCollider2D>().size.x;
    }
    private void Update()
    {
        if (transform.position.x > rightLimit)
        {
            direction = -1;
        }
        else if (transform.position.x < leftLimit)
        {
            direction = 1;
        }
        movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.parent.Translate(movement);
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("MovingPlatform.cs: Platform hit player");

            playerManager.Bounce();

            playerManager.CanDoubleJump(true);
        }
    }
}
