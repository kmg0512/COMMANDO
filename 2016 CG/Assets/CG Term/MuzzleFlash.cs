using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

    public Light light;
    public EllipsoidParticleEmitter emitter;

    private AudioSource audio = null;

    private float lightCount = 0;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        if (light == null) Debug.LogError("MuzzleFlash : Light not found.");
        if (emitter == null) Debug.LogError("MuzzleFlash : Emitter not found.");
        if (audio == null) Debug.LogError("MuzzleFlash : Audio Source not found.");
	}
	
	// Update is called once per frame
	void Update () {
	    if(light.enabled)
        {
            lightCount += Time.deltaTime;
        }

        if (lightCount > 0.03f)
        {
            lightCount = 0;
            light.enabled = false;
        }
	}

    public void Emit()
    {
        emitter.Emit();
        audio.Play();
        //light.enabled = true;
    }
}
