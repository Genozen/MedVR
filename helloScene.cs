using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class helloScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string waitingRoom;
    public string room1;
    public string room2;
    public string room3;

    public void switchScene()
    {
        if (SceneManager.GetSceneByName(waitingRoom).isLoaded)
            SceneManager.UnloadSceneAsync(waitingRoom);

        if (!SceneManager.GetSceneByName(room1).isLoaded)
            SceneManager.LoadScene(room1, LoadSceneMode.Additive);
    }

    public void toRoom1()
    {
        if (SceneManager.GetSceneByName(room2).isLoaded)
            SceneManager.UnloadSceneAsync(room2);
        if (SceneManager.GetSceneByName(room3).isLoaded)
            SceneManager.UnloadSceneAsync(room3);
        if (SceneManager.GetSceneByName(waitingRoom).isLoaded)
            SceneManager.UnloadSceneAsync(waitingRoom);

        if (!SceneManager.GetSceneByName(room1).isLoaded)
            SceneManager.LoadScene(room1, LoadSceneMode.Additive);
    }
    public void toRoom2()
    {
        if (SceneManager.GetSceneByName(room1).isLoaded)
            SceneManager.UnloadSceneAsync(room1);
        if (SceneManager.GetSceneByName(room3).isLoaded)
            SceneManager.UnloadSceneAsync(room3);
        if (SceneManager.GetSceneByName(waitingRoom).isLoaded)
            SceneManager.UnloadSceneAsync(waitingRoom);

        if (!SceneManager.GetSceneByName(room2).isLoaded)
            SceneManager.LoadScene(room2, LoadSceneMode.Additive);
    }
    public void toRoom3()
    {
        if (SceneManager.GetSceneByName(room1).isLoaded)
            SceneManager.UnloadSceneAsync(room1);
        if (SceneManager.GetSceneByName(room2).isLoaded)
            SceneManager.UnloadSceneAsync(room2);
        if (SceneManager.GetSceneByName(waitingRoom).isLoaded)
            SceneManager.UnloadSceneAsync(waitingRoom);

        if (!SceneManager.GetSceneByName(room3).isLoaded)
            SceneManager.LoadScene(room3, LoadSceneMode.Additive);
    }
    public void toWaitingRoom()
    {
        if (SceneManager.GetSceneByName(room1).isLoaded)
            SceneManager.UnloadSceneAsync(room1);
        if (SceneManager.GetSceneByName(room2).isLoaded)
            SceneManager.UnloadSceneAsync(room2);
        if (SceneManager.GetSceneByName(room3).isLoaded)
            SceneManager.UnloadSceneAsync(room3);

        if (!SceneManager.GetSceneByName(waitingRoom).isLoaded)
            SceneManager.LoadScene(waitingRoom, LoadSceneMode.Additive);
    }
}