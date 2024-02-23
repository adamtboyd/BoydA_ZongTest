//Code by Adam Boyd for Zong Programming Test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Scene_Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene_InteractiveObjects", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
