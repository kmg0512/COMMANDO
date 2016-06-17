using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    #region Singleton
    private static Spawner m_instance = null;
    public static Spawner Instance
    {
        get
        {
            if (m_instance == null)
                Debug.LogError("Spawner has not awaken.");

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
    #endregion

    public GameObject yukari;
    public GameObject yukariSpawnPosition;
    public GameObject creeper;
    public GameObject enderman;
    public GameObject[] spawnPoints;

    private GameObject m_yukariRef;
    private List<GameObject> m_shootableList;         // Shootables
    private List<GameObject> m_hitList;               // Hit 당한 Shootable들

    // Use this for initialization
    void Start () {
        if (yukari == null) Debug.LogError("Spawner : Yukari not found.");
        if (yukariSpawnPosition == null) Debug.LogError("Spawner : Yukari Spawn Point not found.");
        if (creeper == null) Debug.LogError("Spawner : Creeper not found.");
        if (enderman == null) Debug.LogError("Spawner : Enderman not found.");
        m_shootableList = new List<GameObject>();
        m_hitList = new List<GameObject>();
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
            if(m_yukariRef != null) go.GetComponent<CreeperController>().yukari = m_yukariRef;
            m_shootableList.Add(go);
        }
    }

    public void SpawnCreeper()
    {
        int spawnPointLength = spawnPoints.Length;
        int index = Random.Range(0, spawnPointLength);
        GameObject point = spawnPoints[index];

        SpawnCreeper(point);
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

    public void SpawnYukari()
    {
        if(yukari != null)
        {
            GameObject go = Instantiate(yukari);
            if(yukariSpawnPosition != null)
            {
                Vector3 position = yukariSpawnPosition.transform.position;
                Quaternion rotation = yukariSpawnPosition.transform.rotation;
                go.transform.position = new Vector3(position.x, position.y, position.z);
                go.transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            }
            m_yukariRef = go;
            InputManager.Instance.SetYukari(m_yukariRef);
        }
    }

    public void MessageDead(GameObject victim)
    {
        if(m_shootableList.Contains(victim))
        {
            Destroy(victim);
            m_shootableList.Remove(victim);
        }
    }
}
