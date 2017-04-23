using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character_Controller : MonoBehaviour {
    public Vector2 location = new Vector2(0, 0);
    public Transform TileMap;
    private Rigidbody2D rb;
    private TileMap map;

    public void Start(){
        rb = GetComponent<Rigidbody2D>();
        map = TileMap.GetComponent<TileMap>();
    }

    public void Update(){
        int tempX = 0, tempY = 0;
        if (Input.GetKeyDown(KeyCode.W)){
            tempY += 1;
        }
        if (Input.GetKeyDown(KeyCode.A)){
            tempX -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S)){
            tempY -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D)){
            tempX += 1;
        }
        int xx = (int)location.x + tempX;
        int yy = (int)location.y + tempY;
        if(!map.getTile(xx, yy).isSolid(xx, yy) && map.getTile(xx,yy) != Tile.emptyTile){
            location.Set(xx, yy);
        }


        //final step
        if (transform.position.x != location.x || transform.position.y != location.y){
            transform.position = location;
        }
    }

    public void attack(){

    }
}
