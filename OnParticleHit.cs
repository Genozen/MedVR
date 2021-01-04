using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleHit : MonoBehaviour {

    private GameObject child;
    private ParticleSystem particle;
    private float timeLeft = 1f;
    private bool isDestroyed = false;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        child = gameObject.transform.GetChild(0).gameObject; //grabs the "default" child object
        particle = gameObject.transform.Find("Particle revolver 002 Eternal charge").GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        if (checkDestroyed()) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnParticleCollision(GameObject other) {
        Debug.Log("Particle Hit!!");
        Destroy(child);
        isDestroyed = true;
        particle.Play();
        audio.Play();
        SteamVR_Controller.Input(3).TriggerHapticPulse(1000);
        SteamVR_Controller.Input(4).TriggerHapticPulse(1000);
    }

    bool checkDestroyed() {
        return isDestroyed;
    }


}
