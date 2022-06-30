using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GameObject roomsData;
    private GameObject templates;
    public int level;
    private GameObject[] enemySpawned;
    public bool levelFinish=false;
    private Vector2[] positions= {new Vector2( 0, 0 )};
    public int countEnemy=0;
    public bool spawned=false;
    // Start is called before the first frame update
    void Start()
    {
        this.level=1;
        GenerateMap(1);
    }
    public void LevelUp(){
        this.level=this.level+1;
    }
    public void ResetLevelFinish(){
        this.levelFinish=false;
    }
    public void GenerateMap(int lvl){
        roomsData=Instantiate(Resources.Load("RoomsData", typeof(GameObject))) as GameObject;
        roomsData.GetComponent<RoomsData>().Init(lvl,lvl);
        Instantiate(Resources.Load("NESW",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);
        Instantiate(Resources.Load("Floor",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);
        //GameObject.FindGameObjectWithTag("RoomsData").GetComponent<RoomsData>().wait(5);
        Invoke("ChangeRooms",1f);
        Invoke("Spawn",2f);
        Invoke("DestroyAllSpawnPoints",1f);
        //Instantiate(Resources.Load("Floor",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);
    }

    void Update()
    {
        if(this.spawned)
        CheckEnemyAlive();
    }
    void CheckEnemyAlive()
    {
        if (this.levelFinish==false)
        {
            this.enemySpawned=GameObject.FindGameObjectsWithTag("Enemy1");
            this.countEnemy=0;
            foreach(GameObject gameobj in this.enemySpawned)
            {
                if(gameobj.active)
                {

                    this.countEnemy=this.countEnemy+1;
                }
            }
            if(this.countEnemy==0)
            {
                print("ioDovreiInstanziare");
                Instantiate(Resources.Load("Exit",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);
                this.levelFinish=true;
                this.spawned=false;
            }
        }
    }
    void DestroyAllSpawnPoints(){
            foreach(GameObject gameobj in GameObject.FindGameObjectsWithTag("SpawnPoint"))
            {
                Destroy(gameobj);
            }
    }

    void ChangeRooms()
    {
        this.roomsData.GetComponent<RoomsData>().ChangeRoom();
    }
    void Spawn()
    {
        for (int x=0 ; x<roomsData.GetComponent<RoomsData>().positions.Count;x++ )
        this.roomsData.GetComponent<RoomsData>().SpawnStuffs(roomsData.GetComponent<RoomsData>().positions[x],GameObject.FindGameObjectWithTag("Templates").GetComponent<Templates>().randomFloorTile,20);
        for (int x=0 ; x<roomsData.GetComponent<RoomsData>().positions.Count;x++ )
        this.roomsData.GetComponent<RoomsData>().SpawnStuffsNoRotation(roomsData.GetComponent<RoomsData>().positions[x],GameObject.FindGameObjectWithTag("Templates").GetComponent<Templates>().enemyArray,5);
        this.spawned=true;
        for (int x=0 ; x<roomsData.GetComponent<RoomsData>().positions.Count;x++ )
        this.roomsData.GetComponent<RoomsData>().SpawnStuffsNoRotation(roomsData.GetComponent<RoomsData>().positions[x],GameObject.FindGameObjectWithTag("Templates").GetComponent<Templates>().powerUp,15);
    }
}
