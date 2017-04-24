using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class LevelLoader : MonoBehaviour {
    public Transform playerPrefab;
    public Transform TurretPrefab;
    public List<Texture2D> levels;
    //
    private int currentLevel = 0;



    private void Start(){
        LoadMap();
    }


    public void ClearMap(){
        GameManager.manager.map.clearMapObjects();
    }

    public void LoadMap(){
        ClearMap();
        Color32[] allPixels = levels.ElementAt<Texture2D>(currentLevel).GetPixels32();
        int width = levels.ElementAt<Texture2D>(currentLevel).width;
        int height = levels.ElementAt<Texture2D>(currentLevel).height;

        GameManager.manager.map.resize(width, height);

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Color32 color = allPixels[x + y * width];
                //convert the Color32 into a single in RGBA
                uint colorValue = (uint)((color.r << 24) | (color.g << 16) | (color.b << 8) | color.a);
                Debug.Log("x: " + x + " y: " + y + " color: " +  colorValue);
                if (colorValue == 0x000000FF) { // wall
                    Debug.Log("wall");
                    GameManager.manager.map.setTile(x, y, Tile.wallTile);
                } else if (colorValue == 0xffffffff) { //floor
                    Debug.Log("floor");
                    GameManager.manager.map.setTile(x, y, Tile.floorTile);
                } else if (colorValue == 0x00FF00FF){ // player 
                    GameManager.manager.setPlayerSpawn(x, y);
                    Instantiate(playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }else if (color.r == 0xFF && color.g == 0xFF && color.a == 0xFF) {//turret
                   Transform TF = Instantiate(TurretPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    TF.GetComponent<Cannon_Mob>().dir = color.b;
               }else if (colorValue == 0xFF0000FF){
                    GameManager.manager.map.setTile(x, y, Tile.finishTile);
                }
            }
        }
        GameManager.manager.map.buildMapObjects();
    }
}
