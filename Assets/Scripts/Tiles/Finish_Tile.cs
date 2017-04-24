using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Tile : Tile{

    public Finish_Tile(int id) : base(id){}

    public override int getSpriteIndex(int dir, int data){
        return 3;
    }

    public override bool isSolid(int x, int y){
        return false;
    }
}
