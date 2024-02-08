using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Script : MonoBehaviour
{
    public SCENE nextScene;
    public int SceneLivingSeconds;
    private float currentTime=0;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+=Time.deltaTime;
        if(currentTime>=SceneLivingSeconds){
            currentTime=0;
            changeScene();
        }
    }

    public void changeScene(){
        changeScene(nextScene);
    }

    public void changeScene(SCENE nextScene){
        SceneManager.LoadSceneAsync(nextScene.ToString());
    }

    public void SetSceneLivingSeconds(int seconds){
        SceneLivingSeconds=seconds;
    }
}
