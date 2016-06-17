
public class Phase {

    private readonly int PHASETIME = 15;

    private float m_gameTime;

    private int m_phaseNum;
    private int m_maxCreeper;

    #region Properties
    public int CurrentPhase { get { return m_phaseNum; } }
    public int MaxCreeper { get { return m_maxCreeper; } }
    public float GameTime { get { return m_gameTime; } }
    #endregion

    public Phase()
    {
        m_gameTime = 0;
        m_phaseNum = -1;
        m_maxCreeper = SetMaxCreeper();
    }

    public bool Update(float deltaTime)
    {
        m_gameTime += deltaTime;

        float diff = m_gameTime - (m_phaseNum * PHASETIME);
        if(diff > (m_phaseNum+1) * PHASETIME)
        {
            StartNextPhase();
            return true;
        }

        return false;
    }

    private void StartNextPhase()
    {
        m_phaseNum += 1;
        SetMaxCreeper();
    }

    private int SetMaxCreeper()
    {
        int newMaxCreeper = (m_phaseNum + 1) * 2;
        m_maxCreeper = newMaxCreeper;
        return newMaxCreeper;
    }
}
