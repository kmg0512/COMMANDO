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
    public float speed = 5f;
    public float shootDelay = 0.12f;
    public float damage = 20f;
    public MuzzleFlash muzzleFlash;

    private CharacterController m_controller;
    private YukariState m_state;
    private float m_readyTime;
    private float m_delayedTime;
    private float m_focusAngle;
    private float m_currentFocusAngle;

	// Use this for initialization
	void Start () {
        if (!animator) Debug.LogError("Yuzuki Yukari : Animator not found.");
        m_controller = GetComponent<CharacterController>();
        if (!m_controller) Debug.LogError("Yuzuki Yukari : Character Controller not found.");

        m_state = YukariState.Ready;
        m_readyTime = 0;
        m_delayedTime = shootDelay;
        m_focusAngle = 0;
        m_currentFocusAngle = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Ready 상태일 때 Active 상태로 전환한다.
        m_state = ReadyStateTransition();

        // 딜레이 카운트
        CountDelay();

        // 바라보는 방향 선형 보간
        m_currentFocusAngle = Mathf.Lerp(m_currentFocusAngle, m_focusAngle, 0.1f);
        animator.SetFloat("Focus", m_currentFocusAngle);
	}

    private YukariState ReadyStateTransition()
    {
        if(m_state == YukariState.Ready)
        {
            if(m_readyTime > 1.866f)
            {
                m_readyTime = 0;
                animator.SetFloat("Ready", 1.0f);
                return YukariState.Active;
            }
            m_readyTime += Time.deltaTime;
        }

        return YukariState.Ready;
    }

    private void CountDelay()
    {
        if (m_delayedTime < shootDelay)
        {
            m_delayedTime += Time.deltaTime;
        }
        else if (m_delayedTime > shootDelay)
        {
            m_delayedTime = shootDelay;
        }
    }

    private void Shoot(RaycastHit hit)
    {
        Shootable shootable = hit.transform.GetComponent<Shootable>();
        if(shootable != null)
        {
            Spawner.Instance.HitShootable(hit.transform.gameObject);
        }
    }

    public void HitSomething(RaycastHit hit)
    {
        if(m_delayedTime == shootDelay)
        {
            ///////////////////////////////
            // 애니메이션 및 사운드
            ///////////////////////////////

            // 사격 애니메이션
            animator.SetTrigger("Fire");
            if (muzzleFlash != null) muzzleFlash.Emit();

            // 바라보는 방향
            Vector3 focus = (hit.point - transform.position).normalized;
            Vector3 forward = transform.forward;
            Vector3 forward2d = new Vector3(forward.x, 0, forward.z);
            Vector3 focus2d = new Vector3(focus.x, 0, focus.z);
            forward2d.Normalize();
            focus2d.Normalize();
            Vector3 cross = Vector3.Cross(forward2d, focus2d);
            float angle = cross.sqrMagnitude;
            if(cross.y < 0) angle *= -1;
            m_focusAngle = angle;

            // 발사
            Shoot(hit);

            // 카운트 초기화
            m_delayedTime = 0;
        }
    }
}
