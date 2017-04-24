using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mob : Moving_Mob{

    //public List<Sprite> dirSprites;
    private SpriteRenderer sr;
    public int maxHealth = 100;
    // 
    private int health;
    private Vector2 end;
    private bool waitingforEOT = false; // waiting for end of turn 
    //
    protected override void Start(){
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
        end = new Vector2(transform.position.x, transform.position.y);
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

        if(Input.GetKeyDown(KeyCode.R)){
            GameManager.manager.map.clearMapObjects();
        }

        if (waitingforEOT
            && transform.position.x == end.x
            && transform.position.y == end.y){
            waitingforEOT = false;
            GameManager.manager.StepWorld();

        }

        if (xt != 0 || yt != 0){
            if(transform.position.x == end.x && transform.position.y == end.y && !waitingforEOT) {
                //if (yt == 1){
                //    sr.sprite = dirSprites.ElementAt<Sprite>(3);
                //}
                //if (xt == -1) {
                //    sr.sprite = dirSprites.ElementAt<Sprite>(1);
                //}
                //if (yt == -1){
                //    sr.sprite = dirSprites.ElementAt<Sprite>(0);
                //}
                //if (xt == 1){
                //    sr.sprite = dirSprites.ElementAt<Sprite>(2);
                //}
                if(Move(xt, yt)){
                    waitingforEOT = true; 
                    end.Set(xt + transform.position.x, yt + transform.position.y);
                }
            }else {/*Do nothing*/}
        }
    }

    public void WaitTurn(){
        GameManager.manager.StepWorld();
    }

    protected override void onMobCollision(Moving_Mob other){}

    protected override void onTileCollision(int x, int y, Tile tile){}
    protected override void onTileTouch(int x, int y, Tile tile){
      if(tile is Finish_Tile){
            GameManager.manager.NextLevel();
        }
    }

    public override void Tick() {
        // I don't need to do anything because I am the player
    }

    public override bool isSolid(Moving_Mob mob){
        return true;
    }

    public void die(){
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

