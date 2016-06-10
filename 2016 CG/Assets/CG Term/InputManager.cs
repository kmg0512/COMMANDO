using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private static InputManager m_instance = null;
    public static InputManager Instance
    {
        get
        {
            if (m_instance == null)
                Debug.LogError("InputManager has not awaken.");

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

    public GameObject yukari;

	// Use this for initialization
	void Start () {
        if (yukari == null) Debug.LogError("InputManager : It requires yukari reference.");
	}

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            // 타격판정
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 유카리에게 hit 정보를 보내고 할 일 끝
                if (yukari != null) yukari.GetComponent<YukariController>().HitSomething(hit);
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // 타격판정
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // 유카리에게 hit 정보를 보내고 할 일 끝
                    if (yukari != null) yukari.GetComponent<YukariController>().HitSomething(hit);
                }
            }
        }
#endif
    }
}
