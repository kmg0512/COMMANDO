using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private static Spawner m_instance = null;
    public static Spawner Instance
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
        if (m_instance != null)
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
    public GameObject creeper;
    public GameObject enderman;
    public GameObject[] spawnPoints;
    
    private List<GameObject> m_shootableList;         // Shootables
    private List<GameObject> m_hitList;               // Hit 당한 Shootable들

    // Use this for initialization
    void Start () {
        if (yukari == null) Debug.LogError("Spawner : It requires Yukari object reference.");
        m_shootableList = new List<GameObject>();
        m_hitList = new List<GameObject>();

        SpawnCreeper(spawnPoints[0]);
	}
	
	// Update is called once per frame
	void Update () {

        // 각 Shootable 오브젝트의 OnHit 콜백 호출
	    if(m_hitList.Count > 0)
        {
            foreach(GameObject go in m_hitList)
            {
                if(m_shootableList.Contains(go))
                {
                    Shootable shootable = go.GetComponent<Shootable>();
                    if(yukari != null) shootable.OnHit(yukari);
                }
                
            }

            m_hitList.Clear();
        }
	}

    public void HitShootable(GameObject shootable)
    {
        m_hitList.Add(shootable);
    }

    public void SpawnCreeper(GameObject point)
    {
        if (creeper != null)
        {
            GameObject go = Instantiate(creeper);
            go.transform.position = new Vector3(point.transform.position.x, point.transform.position.y, point.transform.position.z);
            go.GetComponent<CreeperController>().yukari = yukari;
            m_shootableList.Add(go);
        }
    }

    public void SpawnEnderman(GameObject point)
    {
        if (enderman != null)
        {
            GameObject go = Instantiate(enderman);
            go.transform.position = new Vector3(point.transform.position.x, point.transform.position.y, point.transform.position.z);
            m_shootableList.Add(go);
        }
    }
}
