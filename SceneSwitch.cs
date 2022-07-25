using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneSwitch : MonoBehaviour{
    
    public float changeTime;
    public string sceneName;

    void Update(){
        changeTime -= Time.deltaTime;
        if(changeTime <=0){
            SceneManager.LoadScene(sceneName);
        }

    }
}
