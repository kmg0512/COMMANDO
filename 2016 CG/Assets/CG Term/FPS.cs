using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

    Text m_text;
    float m_timer;

	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
        m_timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        
        if(m_timer > 1f)
        {
            m_timer = 0f;
            m_text.text = (1 / Time.deltaTime).ToString();
        }
	}
}
