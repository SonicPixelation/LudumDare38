using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mob : Moving_Mob{

    public List<Sprite> dirSprites;
    private SpriteRenderer sr;
    //private bool waitingforEOT = false; // waiting for end of turn 
    public int maxHealth = 100;
    // 
    private int health;

    protected override void Start(){
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    private void Update(){
        int xt = 0, yt = 0;
        if(Input.GetKeyDown(KeyCode.W)){
            yt += 1;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            xt -= 1;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            yt -= 1;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            xt += 1;
        }

        //other biz
        if(Input.GetKeyDown(KeyCode.Space)){
            WaitTurn();
        }
        
        //if(waitingforEOT 
        //    && transform.position.x % 1f == 0 
        //    && transform.position.y %1f == 0){
        //    waitingforEOT = false;
        //    Debug.Log("EOT Finish");
        //    GameManager.manager.StepWorld();
        //}

        if(xt != 0 || yt != 0){
            if((transform.position.x % 1f) == 0 && (transform.position.y % 1f) == 0) {
                if (yt == 1){
                    sr.sprite = dirSprites.ElementAt<Sprite>(3);
                }
                if (xt == -1) {
                    sr.sprite = dirSprites.ElementAt<Sprite>(1);
                }
                if (yt == -1){
                    sr.sprite = dirSprites.ElementAt<Sprite>(0);
                }
                if (xt == 1){
                    sr.sprite = dirSprites.ElementAt<Sprite>(2);
                }
                if(Move(xt, yt)){
                    GameManager.manager.StepWorld();
                }
            }else {/*Do nothing*/}
        }
    }

    public void WaitTurn(){
        GameManager.manager.StepWorld();
    }

    protected override void onMobCollision(Moving_Mob other){}

    protected override void onTileCollision(int x, int y){}

    public override void Tick() {
        // I don't need to do anything because I am the player
    }

    public override bool isSolid(Moving_Mob mob){
        return true;
    }

    private void die(){
        //TODO: add death animation etc
        remove();
    }

    public void heal(int value) {
        int temp = health + value;
        if(temp > maxHealth) { health = maxHealth; }
        else { health = temp; }
    }

    public void harm(int value) {
        int temp = health - value;
        if(temp <= 0 ) { health = 0; die();}
        else { health = temp;}
    }

    public int getHealth(){ return health;}
}

