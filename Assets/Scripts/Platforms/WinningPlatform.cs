using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPlatform : MonoBehaviour
{
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = LevelManager.Instance.player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("WinningPlatform.cs: Platform hit player");

            LevelManager.Instance.TriggerWinCondition();
        }
    }
}
