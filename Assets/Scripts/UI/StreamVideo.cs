using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private VideoPlayer videoPlayer;
    //private AudioSource audioSource;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
        
    }
    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        Debug.Log("Playing");
        //audioSource.Play();
    }

}
