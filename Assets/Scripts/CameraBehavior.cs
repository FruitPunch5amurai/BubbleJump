using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    LevelManager m_levelManager;
    [SerializeField]
    float m_distanceUntilMove;
    [SerializeField]
    private bool m_cameraIsMoving;


    // Start is called before the first frame update
    void Start()
    {
        m_levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
//        float distanceFromBallY = Mathf.Abs(transform.position.y - m_levelManager.player.transform.position.y);
        if (m_levelManager.player.transform.position.y > transform.position.y)
        {
            m_cameraIsMoving = true;
            transform.position= new Vector3(0.0f,m_levelManager.player.transform.position.y, -10);
            //m_levelManager.transform.position = -m_levelManager.player.transform.position;
            //Move Platforms Down
            //m_levelManager.transform.position = Vector3.Lerp(m_levelManager.transform.position,
            //new Vector3(m_levelManager.transform.position.x, m_levelManager.transform.position.y - 1.0f, m_levelManager.transform.position.z),
            //Time.fixedDeltaTime*3);
            m_levelManager.IncrementPlayerScore();
        }
        else
        {
            m_cameraIsMoving = false;
        }
    }
    public bool IsCameraMoving()
    {
        return m_cameraIsMoving;
    }
}
