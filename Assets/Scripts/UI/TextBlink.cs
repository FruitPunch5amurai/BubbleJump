using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [SerializeField]
    private Image text;
    [SerializeField]
    private float min;
    [SerializeField]
    private float max;
    [SerializeField]
    private float duration;
    float lerp = 0f;
    private float m_min;
    private float m_max;
    // Start is called before the first frame update
    void Start()
    {
        m_min = min;
        m_max = max;
    }

    // Update is called once per frame
    void Update()
    {

        lerp += Time.deltaTime / duration;
        float a = Mathf.Lerp(m_min, m_max, lerp);

        text.color = new Color(text.color.r,text.color.g,text.color.b,a);
        if(a >= max)
        {
            float temp = m_min;
            m_min = m_max;
            m_max = temp;
            lerp = 0;
        }else if(a <= min){
            float temp = m_min;
            m_min = m_max;
            m_max = temp;
            lerp = 0;
        }
    }
}
