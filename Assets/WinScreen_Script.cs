﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen_Script : MonoBehaviour {

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(0);
        }
    }
}
