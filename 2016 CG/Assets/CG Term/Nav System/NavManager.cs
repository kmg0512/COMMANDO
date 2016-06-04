using UnityEngine;
using System.Collections;

public class NavManager : MonoBehaviour {

    #region Singleton
    private static NavManager m_instance = null;
    public static NavManager Instance
    {
        get
        {
            if (m_instance == null) Debug.LogError("NavManager has not awaken.");
            return m_instance;
        }
    }

    void Awake()
    {
        if(m_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            m_instance = this;
        }
    }
    #endregion

    public GameObject yukari;
    private YukariController m_controller;

    // Use this for initialization
    void Start () {
        if (yukari == null)
        {
            Debug.LogError("NavManager : Yukari not found!");
        }
        else
        {
            m_controller = yukari.GetComponent<YukariController>();
            if(m_controller == null)
            {
                Debug.LogError("NavManager ; Yukari Controller not found!");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
