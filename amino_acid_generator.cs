using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class amino_acid_generator : MonoBehaviour {

    public VRTK.GrabAttachMechanics.VRTK_TrackObjectGrabAttach trackObjectScript;
    Material[] mats = new Material[4];
    GameObject mRNA;
    GameObject tRNA;
    GameObject mRNA_base;
    GameObject tRNA_base;
    public GameObject mRNA_baseeee;
    int[] mRNA_seq = new int[30];
    int matches = 0;
    float scale = 0.2f;

    void setup()
    {
        mats[0] = Resources.Load("Nucleotide A", typeof(Material)) as Material;
        mats[1] = Resources.Load("Nucleotide C", typeof(Material)) as Material;
        mats[2] = Resources.Load("Nucleotide U", typeof(Material)) as Material;
        mats[3] = Resources.Load("Nucleotide G", typeof(Material)) as Material;
        mRNA_base = (GameObject)Resources.Load("mRNA_base_prefab");
        tRNA_base = (GameObject)Resources.Load("tRNA_base_prefab");
    }
    GameObject generate_mRNA()
    {
        // Wrapper Object, to be returned
        GameObject parentMRNA = new GameObject("GeneratedMRNA");
        // Generic Object, used to generate children
        GameObject baseMaster;

        // Generate and manipulate each base
        for (int i = 0; i < 30; i++)
        {
            mRNA_seq[i] = Random.Range(0, 4);
            //baseMaster = GameObject.CreatePrimitive(PrimitiveType.Cube);
            baseMaster = Instantiate(mRNA_base);
            // Transformations
            //baseMaster.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            baseMaster.transform.localPosition = new Vector3(0, 0, i);
            // Materials
            baseMaster.GetComponent<Renderer>().sharedMaterial = mats[mRNA_seq[i]];
            baseMaster.transform.Find("default").GetComponent<Renderer>().sharedMaterial = mats[mRNA_seq[i]];
            
            baseMaster.transform.parent = parentMRNA.transform;
        }

        // Manipulate the entire mRNA
        parentMRNA.transform.localScale = new Vector3(scale, scale, scale);
        parentMRNA.transform.position = GameObject.Find("ribosome_split").transform.position + new Vector3(0, 1.0f, -2f);

        return parentMRNA;
    }
    GameObject generate_tRNA(int[] seq)
    {
        // Wrapper Object, to be returned
        GameObject parentTRNA = new GameObject("GeneratedTRNA");
        // Generic Object, used to generate children
        GameObject baseMaster;

        // Generate and manipulate each base
        for (int i = 0; i < 3; i++)
        {
            //baseMaster = GameObject.CreatePrimitive(PrimitiveType.Cube);
            baseMaster = Instantiate(tRNA_base);
            // Transformations
            //baseMaster.transform.localScale = new Vector3(2, 2, 2);
            baseMaster.transform.localPosition = new Vector3(0, 0, i);
            // Materials
            baseMaster.GetComponent<Renderer>().sharedMaterial = mats[seq[i]];
            baseMaster.transform.Find("default").GetComponent<Renderer>().sharedMaterial = mats[seq[i]];

            baseMaster.transform.parent = parentTRNA.transform;
        }

        // Manipulate the entire tRNA
        parentTRNA.transform.Rotate(0f, 270, 0f);

        return parentTRNA;
    }
    GameObject set_tRNAs()
    {
        // Wrapper Object
        GameObject parentTRNAs = new GameObject("GeneratedTRNAs");
        // Generic Object, used to generate children
        GameObject tRNAMaster;
        // Return Object, used to verify correct tRNA
        GameObject tRNAAnswer;
        // Generic sequence, used to select bases in tRNA
        int[] seq;
        // Actual sequence, used to match tRNA to mRNA
        int answer = Random.Range(0, 5);

        // Generate and manipulate each tRNA
        for (int i = 0; i < 5; i++)
        {
            if (i == answer) seq = new int[] { mRNA_seq[matches * 3], mRNA_seq[matches * 3 + 1], mRNA_seq[matches * 3 + 2] };
            else seq = new int[] { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
            tRNAMaster = generate_tRNA(seq);
            // Transformations
            tRNAMaster.transform.localScale = new Vector3(1, 1, 1);
            tRNAMaster.transform.localPosition = new Vector3(10 - Random.Range(0f, 20f), 1, 10 * (i - 2.5f) + 0.25f - Random.Range(0f, 0.5f));
            tRNAMaster.transform.Rotate(0f, 5 - Random.Range(0f, 10f), 0f);
            // Colliders
            // Need to add Cube Collider, VRTK_Interactable Object, VRTK_Track Object Grab Attach, VRTK_Swap Controller Grab Action, RigidBody, VRTK_Interact Haptics
            tRNAMaster.AddComponent<MeshCollider>();
            tRNAMaster.AddComponent<VRTK_InteractableObject>();
            tRNAMaster.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
            tRNAMaster.GetComponent<VRTK_InteractableObject>().grabOverrideButton = VRTK_ControllerEvents.ButtonAlias.TriggerHairline;
            tRNAMaster.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.green;
            tRNAMaster.GetComponent<VRTK_InteractableObject>().grabAttachMechanicScript = trackObjectScript;


            tRNAMaster.AddComponent<Rigidbody>();
            //tRNAMaster.GetComponent<Rigidbody>().useGravity = false;
            tRNAMaster.AddComponent<VRTK_InteractHaptics>();


            tRNAMaster.transform.parent = parentTRNAs.transform;
            if (i == answer) tRNAAnswer = tRNAMaster;
        }

        // Manipulate the entire set of tRNAs
        parentTRNAs.transform.localScale = new Vector3(scale, scale, scale);
        parentTRNAs.transform.position = new Vector3(2, 10, -6);

        return parentTRNAs;
    }
    bool check_collision()
    {
        return false;
    }
    void moveOn()
    {
        return;
    }

    // Use this for initialization
    void Start()
    {
        setup();
        mRNA = generate_mRNA();
        tRNA = set_tRNAs();
        SceneManager.MoveGameObjectToScene(tRNA, SceneManager.GetSceneByName("Room_Ribosome"));
        SceneManager.MoveGameObjectToScene(mRNA, SceneManager.GetSceneByName("Room_Ribosome"));
    }
    // Update is called once per frame
    void Update () {
        // Check for collision between ribosome_split and GeneratedTRNA
        if (check_collision())
        {
            // Check mRNA sequence to tRNA sequence, or check tRNA ID to tRNAAnswer variable
            // if there's a match...
            Vector3 newVector = new Vector3(0, 0, 3 * 2 * scale);
            mRNA.transform.position -= newVector;
            matches++;
            if (matches >= 5) moveOn();
        }
	}
}
