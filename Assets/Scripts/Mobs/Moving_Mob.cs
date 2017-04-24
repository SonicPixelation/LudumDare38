using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Moving_Mob : MonoBehaviour {
    private static int uniqueId = 0;

    //
    public float moveSpeed = 0.1f;
    private int myId = 0;
    private bool nR = false; //Needs Removing
   
    private float inverseSpeed;
    private Rigidbody2D rb2D;




    protected virtual void Start(){
        myId = Moving_Mob.uniqueId++;
        rb2D = transform.GetComponent<Rigidbody2D>();
        inverseSpeed = 1f / moveSpeed;
        GameManager.manager.addMob(transform);
    }

    protected virtual void remove() {
        nR = true;
    }


    public abstract void Tick();

    protected abstract void onMobCollision(Moving_Mob other);
    protected abstract void onTileCollision(int x, int y, Tile tile);
    protected virtual void onTileTouch(int x, int y, Tile tile){}

    public bool Move(int xDir, int yDir){
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        bool contains = false;
        if(GameManager.manager.map.getTile((int)end.x,(int)end.y).isSolid((int)end.x, (int)end.y) 
                || GameManager.manager.map.getTile((int)end.x,(int)end.y) == Tile.emptyTile 
                || (contains = GameManager.manager.containsMob((int)end.x, (int)end.y))){

                if(contains){
                Moving_Mob temp = GameManager.manager.getMob((int)end.x, (int)end.y).GetComponent<Moving_Mob>();
                onMobCollision(temp);
                temp.onMobCollision(this);
                if (!temp.isSolid((this)) || !isSolid(temp)){
                    StartCoroutine(SmoothMove(end));
                    return true;
                }
            }else{
                onTileCollision((int)end.x, (int)end.y, GameManager.manager.map.getTile((int) end.x, (int)end.y));
            }
            return false;           
        }
        onTileTouch((int)end.x, (int)end.y, GameManager.manager.map.getTile((int)end.x, (int)end.y));
        StartCoroutine(SmoothMove(end));
        return true;
    }
    protected IEnumerator SmoothMove(Vector3 end) {
        float sqrtRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrtRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseSpeed * Time.deltaTime);

            rb2D.MovePosition(newPosition);

            sqrtRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
        transform.position = new Vector3(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
    }

    //getters
    public int getMyId() { return myId; }
    public abstract bool isSolid(Moving_Mob mob); 
    public bool needsRemoveing() { return nR; }
}

