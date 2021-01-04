using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour {

    private GameObject virus;
    public GameObject virusHolder;
    // Use this for initialization
    void Start () {
        virus = (GameObject)Resources.Load("HIV Retrovirus purple");
        generate();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("HIV Retrovirus purple(Clone)") == null) 
        {
            generate();
        }
    }

    void generate() {
        for (int i = 0; i < 15; i++)
        {
            GameObject temp = Instantiate(virus, new Vector3(Random.Range(-25f, -5f), Random.Range(0, 15f), Random.Range(-20f, 20f)), Quaternion.identity);
            temp.transform.SetParent(virusHolder.transform);
        }
    }
}
