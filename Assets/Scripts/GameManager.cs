using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    public TileMap map;
    //
    private List<Transform> mobs;
    
    private void Awake(){
        if(manager == null){
            manager = this;
        }else{
            Destroy(gameObject);
        }
        mobs = new List<Transform>();
    }

    public void addMob(Transform mob){
        mobs.Add(mob);
    }

    public void removeMob(Transform mob){
        mobs.Remove(mob);
    }

    private List<Transform> removeList = new List<Transform>();
    public void StepWorld() {
        for(int i = 0; i < mobs.Count(); i++){
                Moving_Mob mm = mobs.ElementAt<Transform>(i).GetComponent<Moving_Mob>();
                mm.Tick();
                if (mm.needsRemoveing()){
                    removeList.Add(mobs.ElementAt<Transform>(i));
            }
        }
        while(removeList.Count != 0){
            removeMob(removeList.ElementAt<Transform>(0));
            Destroy(removeList.ElementAt<Transform>(0).gameObject);
            removeList.RemoveAt(0);

        }
    }
    //getters
    public bool containsMob(int x, int y){
        for(int i = 0; i < mobs.Count(); i++){
           if(mobs.ElementAt<Transform>(i).position.x == x 
                && mobs.ElementAt<Transform>(i).position.y == y){
                return true;
            }
        }
        return false;
    }

    public Transform getMob(int x, int y){
        for(int i = 0; i < mobs.Count(); i++){
            if(mobs.ElementAt<Transform>(i).position.x == x 
                && mobs.ElementAt<Transform>(i).position.y == y){
                return mobs.ElementAt<Transform>(i);
            }
        }
        return null;
    }

    public int getMobCount(){ return mobs.Count(); } 
}
