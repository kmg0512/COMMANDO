using UnityEngine;
using System.Collections;

public class MonsterColorBlend : MonoBehaviour {

    public MeshRenderer[] renderers;
    public float blendTime;

    private float blendTimer;

	// Use this for initialization
	void Start () {
        blendTimer = blendTime;
	}
	
	// Update is called once per frame
	void Update () {
	    if(blendTimer < blendTime)
        {
            blendTimer += Time.deltaTime;
        }
        else if(blendTimer > blendTime)
        {
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material.color = new Color(1, 1, 1);
            }

            blendTimer = blendTime;
        }
	}

    public void Blend()
    {
        foreach(MeshRenderer renderer in renderers)
        {
            renderer.material.color = new Color(1, 0, 0);
        }

        blendTimer = 0;
    }
}
