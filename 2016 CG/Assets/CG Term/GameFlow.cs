using UnityEngine;
using System.Collections;

public enum FlowState
{
    MenuScreen,
    MainGame
}

public class GameFlow : MonoBehaviour {

    #region Singleton
    private static GameFlow m_instance = null;
    public static GameFlow Instance
    {
        get
        {
            if (m_instance == null)
                Debug.LogError("GameFlow has not awaken.");

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

    public GameObject menuScreen;
    public GameObject mainGameScreen;

    private FlowState m_currentFlow;
    private bool m_paused;

    private Phase m_phase;

	// Use this for initialization
	void Start () {
        if (menuScreen == null) Debug.LogError("GameFlow : Menu Screen not found.");
        if (mainGameScreen == null) Debug.LogError("GameFlow : Main Game Screen not found.");

        m_phase = new Phase();
        m_paused = true;

        SetFlow(FlowState.MenuScreen);
    }
	
	// Update is called once per frame
	void Update () {
        if(!m_paused)
        {
            if (m_phase.Update(Time.deltaTime))
            {
                // When next phase starts
                Debug.Log(m_phase.CurrentPhase);
                for (int i = 0; i < m_phase.MaxCreeper; ++i)
                {
                    Spawner.Instance.SpawnCreeper();
                }
            }
        }
	}

    private void DeactivateAll()
    {
        menuScreen.SetActive(false);
        mainGameScreen.SetActive(false);
    }

    public void BUTTON_SET_FLOW_MAIN_GAME()
    {
        SetFlow(FlowState.MainGame);
    }

    public void SetFlow(FlowState flow)
    {
        m_currentFlow = flow;
        DeactivateAll();

        switch(flow)
        {
            case FlowState.MenuScreen:
                menuScreen.SetActive(true);
                break;

            case FlowState.MainGame:
                mainGameScreen.SetActive(true);
                Spawner.Instance.SpawnYukari();
                m_paused = false;
                break;
        }
    }
}
