using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Detection : MonoBehaviour {

    GameObject goal;
    GameObject proton;
    public GameObject[] complex = new GameObject[4];
    public Light[] lights = new Light[4];
    bool[] comPresent = new bool[4];
    bool ETCComplete = false;
    CheckBoolean currentComplex;

	// Use this for initialization
	void Start () {
        //goal.GetComponent<Collider>();
        //proton.GetComponent<Collider>();

        currentComplex = complex[0].GetComponent<CheckBoolean>();
        StartCoroutine(mitoCoach());

        // Initialize colors
        for (int j = 0; j < 4; j++)
            lights[j].color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void setComplexBool(bool b) {
        currentComplex.setBool(b);
    }

    private IEnumerator mitoCoach()
    {
        int i = 0; //track the current complex
        yield return new WaitForSeconds(1.0f); //5 seconds for narrative

        //going over all the complexes
        while (!ETCComplete)
        {
            Debug.Log("Begin ETC!!");
            yield return new WaitForSeconds(1.0f);
            currentComplex = complex[i].GetComponent<CheckBoolean>(); //activate the complex
            lights[i].color = Color.green;

            //initiate the complex for allowing H and NADH
            if(currentComplex.getBool() == false) {
                currentComplex.setBool(true);
                Debug.Log("Open Complex:"+i);
            }

            //the current compelex is activated, wait for user to put in H
            while (currentComplex.getBool() == true) {
                if (currentComplex.getGoal() == true) {
                    Debug.Log("Complex " + i + " Goal!!"); //user puts H in
                    break;
                }
                Debug.Log("Waiting for proton:"+i);
                yield return new WaitForSeconds(3.0f);
            }

            // Turn on lamp
            lights[i].color = Color.blue;
            //complex[i].GetComponent<MeshRenderer>().enabled = false; //disable the renderer of complex, change this to close the pump animation later
            i++; //initiate next complex
            Debug.Log("i="+i);

            //4 total complex to complete
            if (i == 4) {
                ETCComplete = true;
                Debug.Log("game cleared");
                // Change colors to indicate completion
                for (int j = 0; j < 4; j++)
                    lights[j].color = Color.Lerp(Color.red, Color.yellow, 0.5f);
            }

         }

    }
}
