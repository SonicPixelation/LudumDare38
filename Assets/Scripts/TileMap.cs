﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {
    public Vector2 MapSize = new Vector2(10, 10);
    public Texture2D tileSheet;
    public Material tileMat;

    private Sprite[] tileSprites;
    private int[] tiles;
    private int[] data;


    private void Awake(){
        tiles = new int[(int)(MapSize.x * MapSize.y)];
        data  = new int[(int)(MapSize.x * MapSize.y)];
        tileSprites = Resources.LoadAll<Sprite>(tileSheet.name);
        Debug.Log(tileSprites.Length);
        //fill(Tile.floorTile);
        //buildMapObjects();
    }


    public void buildMapObjects(){
        for(int y = 0; y < MapSize.y; y++){
            for(int x = 0; x < MapSize.x; x++){
                int i = (int)(x + y * MapSize.x);
                GameObject tile = new GameObject("tile_" + i);
                tile.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();

                tile.transform.parent = transform;
                if(getTile(x, y).getId() != 0){
                    sr.sprite = tileSprites[getTile(x, y).getSpriteIndex(0, getData(x, y))];
                    sr.material = tileMat;
                }
            }
        }
    }

    public void clearMapObjects(){
        while(transform.childCount > 0){
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    } 

    public void resize(int width, int height){
        clearMapObjects();
        MapSize = new Vector2(width, height);
        tiles = new int[(int)(MapSize.x * MapSize.y)];
        data = new int[(int)(MapSize.x * MapSize.y)];

    }

    //getters
    public Tile getTile(int x, int y){
        if (x < 0 || y < 0 || x >= MapSize.x || y >= MapSize.y) return Tile.emptyTile;
        return Tile.tiles[tiles[(int)(x + y * MapSize.x)]];
    }
    public int getData(int x, int y) {
        if (x < 0 || y < 0 || x >= MapSize.x || y >= MapSize.y) return 0;
        return data[(int)(x + y * MapSize.x)];
    }
    public int getWidth()  { return (int)MapSize.x; }
    public int getHeight() { return (int)MapSize.y; }
    //setters
    public void setTile(int x, int y, Tile tile){
        if (x < 0 || y < 0 || x >= MapSize.x || y >= MapSize.y) return;
        tiles[(int)(x + y * MapSize.x)] = tile.getId(); 
    }
    public void setData(int x, int y, int value){
        if (x < 0 || y < 0 || x >= MapSize.x || y >= MapSize.y) return;
        data[(int)(x + y * MapSize.x)] = value;
    }
    public void fill(Tile tile){
        for(int y = 0; y < MapSize.x; y++){
            for (int x = 0; x < MapSize.x; x++){
                setTile(x, y, tile);
            }
        }
    }
}
