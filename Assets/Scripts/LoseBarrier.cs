using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            LevelManager.Instance.TriggerLoseCodition();
        }
    }
}
