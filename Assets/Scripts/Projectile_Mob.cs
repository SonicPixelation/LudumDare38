using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Mob : Moving_Mob{
    public int damageValue = 1;
    public Vector2 targetDir = new Vector3(0, 0);
    public List<Sprite> ProjectileSprites;
       private SpriteRenderer sr;

    protected override void Start(){
        base.Start();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if (targetDir.y ==  1) { sr.sprite = ProjectileSprites.ElementAt<Sprite>(2); }
        if (targetDir.y == -1) { sr.sprite = ProjectileSprites.ElementAt<Sprite>(0); }
        if (targetDir.x == 1)  { sr.sprite = ProjectileSprites.ElementAt<Sprite>(3); }
        if (targetDir.x == -1) { sr.sprite = ProjectileSprites.ElementAt<Sprite>(1); }
    }

    protected override void onMobCollision(Moving_Mob other) {
        if (other is Player_Mob) {
            Player_Mob tempMob = (Player_Mob)other;
            tempMob.harm(damageValue);
        }
        this.remove();
    }

    protected override void onTileCollision(int x, int y) {
        remove();
    }

    public override bool isSolid(Moving_Mob mob){
        return false;
    }

    public override void Tick(){
        Debug.Log("Bullet ID: " + getMyId() + " updated!");
        if (!Move((int)targetDir.x, (int)targetDir.y)) {
        }
    }
}
