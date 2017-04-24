using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;
    public TileMap map;
    public Transform playerPrefab;
    public Transform TurretPrefab;
    public List<Texture2D> levels;
    //
    private int currentLevel = 0;

    //
    private List<Transform> mobs;
    private Vector2 playerSpawn;

    private void Start(){
        if(manager == null){
            manager = this;
        }else{
            Destroy(gameObject);
        }
        mobs = new List<Transform>();
        LoadMap();
    }

    public void addMob(Transform mob){
        mobs.Add(mob);
    }

    public void removeMob(Transform mob){
        mobs.Remove(mob);
    }

    public void clearAllMobs(){
        while(mobs.Count > 0){
            Transform mob = mobs.ElementAt<Transform>(0);
            removeMob(mob);
            mob.SetParent(null);
            Destroy(mob.gameObject);
        }
    }

    private List<Transform> removeList = new List<Transform>();

    private bool needReset = false;
    public void StepWorld() {
        for(int i = 0; i < mobs.Count(); i++){
                Moving_Mob mm = mobs.ElementAt<Transform>(i).GetComponent<Moving_Mob>();
                mm.Tick();
                if (mm.needsRemoveing()){
                    removeList.Add(mobs.ElementAt<Transform>(i));
            }
        }
        while(removeList.Count != 0){
            Moving_Mob mob = removeList.ElementAt<Transform>(0).GetComponent<Moving_Mob>();
            if(mob is Player_Mob){
                needReset = true;
            }
            removeMob(removeList.ElementAt<Transform>(0));
            Destroy(removeList.ElementAt<Transform>(0).gameObject);
            removeList.RemoveAt(0);
        }
        if(needReset){
            needReset = false;
            LoadMap();
        }
    }

    public void setPlayerSpawn(int x, int y){
        playerSpawn = new Vector2(x, y);
    }

    public void NextLevel(){
        if(currentLevel == levels.Count() - 1){
            SceneManager.LoadScene(2);
        }else {
            currentLevel += 1;
            LoadMap();
        }
    }

    //getters
    public bool containsMob(int x, int y){
        for(int i = 0; i < mobs.Count(); i++){
           if(mobs.ElementAt<Transform>(i).position.x == x 
                && mobs.ElementAt<Transform>(i).position.y == y){
                return true;
            }
        }
        return false;
    }

    public Transform getMob(int x, int y){
        for(int i = 0; i < mobs.Count(); i++){
            if(mobs.ElementAt<Transform>(i).position.x == x 
                && mobs.ElementAt<Transform>(i).position.y == y){
                return mobs.ElementAt<Transform>(i);
            }
        }
        return null;
    }

    public int getMobCount(){ return mobs.Count(); }





    public void ClearMap(){
        map.clearMapObjects();
        clearAllMobs();
    }

    public void LoadMap(){
        ClearMap();
        Color32[] allPixels = levels.ElementAt<Texture2D>(currentLevel).GetPixels32();
        int width = levels.ElementAt<Texture2D>(currentLevel).width;
        int height = levels.ElementAt<Texture2D>(currentLevel).height;

        map.resize(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color32 color = allPixels[x + y * width];
                //convert the Color32 into a single in RGBA
                uint colorValue = (uint)((color.r << 24) | (color.g << 16) | (color.b << 8) | color.a);
                if (colorValue == 0x000000FF)
                { // wall
                    Debug.Log("wall");
                    map.setTile(x, y, Tile.wallTile);
                }
                else if (colorValue == 0xffffffff)
                { //floor
                    Debug.Log("floor");
                    map.setTile(x, y, Tile.floorTile);
                }
                else if (colorValue == 0x00FF00FF)
                { // player 
                    GameManager.manager.setPlayerSpawn(x, y);
                    Transform TF = Instantiate(playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    TF.name = "Doug";
                    map.setTile(x, y, Tile.floorTile);
                }
                else if (color.r == 255 && color.g == 255 && color.a == 255){//turret
                    Transform TF = Instantiate(TurretPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    TF.GetComponent<Cannon_Mob>().dir = color.b;
                    TF.name = "Turret_" + TF.GetComponent<Cannon_Mob>().getMyId();
                }
                else if (colorValue == 0xFF0000FF){
                    GameManager.manager.map.setTile(x, y, Tile.finishTile);
                }else{

                }
            }
        }
        GameManager.manager.map.buildMapObjects();
    }
}
