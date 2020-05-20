using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;

    private Rigidbody2D rb;
    [SerializeField]
    private bool isFlat = true;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float maxFallSpeed;
    [SerializeField]
    private bool canDoubleJump = true;
    [SerializeField]
    private float BounceHeight = 2.0f;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    public GameObject[] ghosts = new GameObject[3];
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private Animator[] animators = new Animator[3];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var cam = Camera.main;

        var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        CreateGhostShips();
        PositionGhostShips();
    }
    void Update()
    {
        SwapShips();
    }
    private void FixedUpdate()
    {
        //Movement
        Vector2 tilt = Input.acceleration;
        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        rb.AddForce(tilt);
        //Clamp speed
        if (rb.velocity.x > maxVelocity)
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        if (rb.velocity.x < -maxVelocity)
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
        if (rb.velocity.y < -maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);

        if (rb.velocity.y > 0 && rb.velocity.y < maxVelocity)
        {
            if (animators[0].GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("Stretch"))
                TriggerIdle();
        }
       //if (Input.touchCount == 1)
       //{
       //    if (Input.GetTouch(0).phase == TouchPhase.Began && canDoubleJump)
       //    {
       //        rb.velocity = new Vector2(rb.velocity.x, 0.0f); // Zero out velocity in the y
       //        Bounce();
       //        canDoubleJump = false;
       //    }
       //}
    }

    public void TriggerIdle()
    {
        foreach(Animator anim in animators)
        {
            anim.SetTrigger("Idle Trigger");
        }
    }
    private void TriggerBounce()
    {
        foreach (Animator anim in animators)
        {
            anim.SetTrigger("Bounce Trigger");
        }
    }
    public void Bounce()
    {
       rb.AddForce(transform.up * BounceHeight);
       TriggerBounce();
    }
    public void CanDoubleJump(bool c)
    {
        canDoubleJump = c;
    }
    #region Ghost Ships
    void CreateGhostShips()
    {
        for (int i = 0; i < 3; i++)
        {
            
            ghosts[i] = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
            ghosts[i].transform.parent = transform;
            animators[i] = ghosts[i].GetComponent<Animator>();
            //DestroyImmediate(ghosts[i]);
        }
    }
    void PositionGhostShips()
    {
        // All ghost positions will be relative to the ships (this) transform,
        // so let's star with that.
        var ghostPosition = transform.position;

        // Middle
        ghosts[0].transform.position = ghostPosition;

        // Right
        ghostPosition.x = transform.position.x + screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[1].transform.position = ghostPosition;

        
        // Left
        ghostPosition.x = transform.position.x - screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[2].transform.position = ghostPosition;

        // All ghost ships should have the same rotation as the main ship
        for (int i = 0; i < 3; i++)
        {
            ghosts[i].transform.rotation = transform.rotation;
        }
    }
    void SwapShips()
    {
        foreach (var ghost in ghosts)
        {
            if (ghost.transform.position.x < screenWidth && ghost.transform.position.x > -screenWidth)
            {
                transform.position = ghost.transform.position;

                break;
            }
        }

        PositionGhostShips();
    }
    #endregion
}
