using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject target;

    private void Start(){
       
        
    }

    private void Update(){

        if(target == null){
            target = GameObject.Find("Doug");
        }else{
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
    }

}
