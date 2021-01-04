using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoolean : MonoBehaviour {

    public bool comPresent;
    public bool goal;

	// Use this for initialization
	void Start () {
        comPresent = false;
        goal = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getBool()
    {
        return comPresent;
    }

    public void setBool(bool b) {
        comPresent = b;
    }

    public bool getGoal() {
        return goal;
    }

    public void setGoal(bool b) {
        goal = b;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().tag == "proton")
        {
            Debug.Log("Goal!!");
            setGoal(true); //scored
            setBool(false); //close the gate
        }
    }
}
