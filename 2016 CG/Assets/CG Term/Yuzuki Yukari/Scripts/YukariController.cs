using UnityEngine;
using System.Collections;

public enum YukariState
{
    Ready,
    Active,
    End
}

public class YukariController : MonoBehaviour {

    public Animator animator;

    private YukariState m_state;
    private float m_animationTime;

	// Use this for initialization
	void Start () {
        if (!animator) Debug.LogError("Yuzuki Yukari : Animator not found.");

        m_state = YukariState.Ready;
        m_animationTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Ready 상태일 때 Active 상태로 전환한다.
        m_state = StateTransition();
	}

    private YukariState StateTransition()
    {
        if(m_state == YukariState.Ready)
        {
            if(m_animationTime > 0.933f)
            {
                m_animationTime = 0;
                animator.SetBool("Ready", true);
                return YukariState.Active;
            }

            m_animationTime += Time.deltaTime;
        }

        return YukariState.Ready;
    }

    public void MoveTo(NavNode node)
    {

    }
}
