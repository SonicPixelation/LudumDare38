using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile{

    public WallTile(int id) : base(id){}



    public override bool isSolid(int x, int y){
        return true;
    }

    public override int getSpriteIndex(int dir, int data){
        return 1;
    }
}
