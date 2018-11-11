using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class StageStart : MonoBehaviour {

    public int map;
    public int stage;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void StartStage()
    {
        Time.timeScale = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);//, UnityEngine.SceneManagement.LoadSceneMode.Single);

        //GameObject.Find("StageGenerator").GetComponent<StageGenerator>().GenerateStage();
    }

    public void StartNextStage()
    {
        Time.timeScale = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
