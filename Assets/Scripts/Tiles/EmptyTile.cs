using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : Tile{

    public EmptyTile(int id) : base(id){}


    public override int getSpriteIndex(int dir, int data){
        return -1;
    }

    public override bool isSolid(int x, int y){
        return false;
    }

}

