using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus_generator : MonoBehaviour {

    // Key features
    public GameObject[] viruses = new GameObject[5];
    public GameObject[] dna = new GameObject[5];
    public GameObject wbc;
    // Track antibody attatchment
    bool[] flagged = new bool[5];
    int flaggedCount;
    // Tune virus and cell behavior
    float th = 2.5f;
    float speed = 0.5f;
    float wbc_speed = 1.0f;
    // Count game events
    int mutations = 0;
    int score = 0;

	// Use this for initialization
	void Start () {
        // Randomize initial virus heights
        float dh;
        for (int i = 0; i < 5; i++)
        {
            dh = Random.Range(0f, 10f);
            viruses[i].transform.Translate(new Vector3(0, dh, 0));
            dna[i].transform.Translate(new Vector3(0, dh, 0));
        }
    }

	// Update is called once per frame
	void Update () {
        // Handle Virus Behavior
        for (int i = 0; i < 5; i++)
        {
            // Flag the virus when shot
            if (Random.Range(0f, 1f) < 0.001)
            {
                if (!flagged[i])
                {
                    score++;
                    speed = speed * 1.15f;
                }
                flagged[i] = true;
            }
            if (flagged[i])
            {
                // wait for wbc
            }
            // Move the virus if unflagged and unreceived
            else if (viruses[i].transform.position.y > th)
            {
                viruses[i].transform.Translate(Vector3.down * speed * Time.deltaTime);
                dna[i].transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            // Move the dna if uninserted
            else if (dna[i].transform.position.y > th - 2.5)
            {
                dna[i].transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            // Replace the virus after insertion
            else
            {
                mutations++;
                float h = Random.Range(17f, 22f);
                viruses[i].transform.position = new Vector3(viruses[i].transform.position.x, h, viruses[i].transform.position.z);
                dna[i].transform.position = new Vector3(dna[i].transform.position.x, h, dna[i].transform.position.z);
            }

        }
        // Handle the WBC Behavior
        // Move WBC back to home
        flaggedCount = 0;
        for (int i = 0; i < 5; i++)
            if (flagged[i]) flaggedCount++;
        if (flaggedCount == 0)
        {
            if (wbc.transform.position.y < 25)
                wbc.transform.Translate(Vector3.up * wbc_speed * Time.deltaTime);
        }
        // Move WBC to closest flagged virus
        else
        {
            int v_index = 0;
            Vector3 minDir = 25*Vector3.up;
            Vector3 newDir;
            for (int i = 0; i < 5; i++)
            {
                if (flagged[i])
                {
                    newDir = viruses[i].transform.position - wbc.transform.position;
                    if (newDir.magnitude < minDir.magnitude)
                    {
                        minDir = newDir;
                        v_index = i;
                    }
                }
            }

            if (minDir.magnitude > 0.5)
            {
                minDir.Normalize();
                wbc.transform.Translate(minDir * wbc_speed * Time.deltaTime);
            }
            else
            {
                //target.Dequeue();
                //flaggedCount--;
                flagged[v_index] = false;
                float h = Random.Range(17f, 22f);
                viruses[v_index].transform.position = new Vector3(viruses[v_index].transform.position.x, h, viruses[v_index].transform.position.z);
                dna[v_index].transform.position = new Vector3(dna[v_index].transform.position.x, h, dna[v_index].transform.position.z);
            }
        }
        // Handle game ending cases
        if (mutations > 10)
        {
            // End game
            Debug.Log(score);
            mutations = 0;
        }
    }

    void collision_handler(GameObject virus)
    {
        int v_index = 0;
        // get index of this virus
        if (virus.transform.position.x < 0)
        {
            if (virus.transform.position.y < 0)
                v_index = 1;
            else if (virus.transform.position.y < 0)
                v_index = 2;
        }
        else if (virus.transform.position.x > 0)
        {
            if (virus.transform.position.x < 0)
                v_index = 4;
            if (virus.transform.position.x < 0)
                v_index = 3;
        }

        // update speed and flag that index
        if (!flagged[v_index])
        {
            score++;
            speed = speed * 1.15f;
        }
        flagged[v_index] = true;
    }
}
