using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTile : Tile {

    public DirtTile(int id) : base(id){}

    //getters
    public override bool isSolid(int x, int y){
        return false;
    }

    public override int getSpriteIndex(int dir, int data){
        //TODO: add varaity
        return 0;
    }
}
