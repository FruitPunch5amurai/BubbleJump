using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    private CameraBehavior m_cameraB;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        Camera cam = Camera.main;
        m_cameraB = cam.GetComponent<CameraBehavior>();
        ActivatePlatforms();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_cameraB.IsCameraMoving())
        {
            ActivatePlatforms();
        }
    }
    public void ActivatePlatforms()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(CheckIfInsideCamera(t));
        }
    }
    protected bool CheckIfInsideCamera(Transform t)
    {
        Camera cam = Camera.main;
        var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if (t.position.y > screenBottomLeft.y && t.position.y < screenTopRight.y)
        {
            return true;
        }
        return false;
    }
}