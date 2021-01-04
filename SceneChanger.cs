using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

    // Use this for initialization
    void Start() {
    }

    void onClick(string intro, string outro) {
        SceneManager.LoadScene(intro);
        SceneManager.UnloadScene(outro);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
