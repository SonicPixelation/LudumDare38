using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Mob : Moving_Mob{

    public Transform bullet;
    public int dir = 0;
    public int fireDelay = 2;

    public override bool isSolid(Moving_Mob mob){
        return true;
    }

    int ticks;
    public override void Tick(){
        if(ticks % fireDelay == 0){
            switch(dir){
                case 1: Fire(0,  -1); break;
                case 2: Fire(-1, -1); break;
                case 3: Fire(-1, 0); break;
                case 4: Fire(-1, 1); break;
                case 5: Fire(0,  1); break;
                case 6: Fire(1,  1); break;
                case 7: Fire(1,  0); break;
                case 8: Fire(1, -1); break; 
            }
        }
        ++ticks;
    }

    public void Fire(int xt, int yt){
        Vector3 spawnPos = new Vector3(transform.position.x + xt, transform.position.y + yt, transform.position.z);
        GameObject obj = (GameObject)Instantiate(bullet.gameObject, spawnPos, Quaternion.identity);
        obj.GetComponent<Projectile_Mob>().targetDir = new Vector2(xt, yt);
        obj.GetComponent<Projectile_Mob>().damageValue = 50;   
    }

    protected override void onMobCollision(Moving_Mob other){}
    protected override void onTileCollision(int x, int y){}
}
