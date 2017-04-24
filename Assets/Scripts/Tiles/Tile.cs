using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile {

    public static Tile[] tiles = new Tile[256]; //why 256 you may ask, well...
    public static Tile emptyTile  = new EmptyTile(0);
    public static Tile floorTile  = new FloorTile(1);
    public static Tile wallTile   = new WallTile(2);
    public static Tile finishTile = new Finish_Tile(3);
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
