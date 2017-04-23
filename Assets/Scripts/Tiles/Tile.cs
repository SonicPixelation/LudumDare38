﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile {

    public static Tile[] tiles = new Tile[256]; //why 256 you may ask, well...
    public static Tile emptyTile = new EmptyTile(0);
    public static Tile dirtTile = new DirtTile(1);
    public static Tile stoneTile  = new StoneTile(2);
    //
    private int id;

    public Tile(int id){
        this.id = id;
        tiles[id] = this;
    }

    //getters
    public abstract bool isSolid(int x, int y);
    public abstract int getSpriteIndex(int dir, int data);

    public int getId() { return id; }
    
}
