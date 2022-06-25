using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GameObject roomsData;
    private GameObject templates;
    private Vector2[] positions= {new Vector2( 0, 0 )};
    private int MapDimension = 1;
    // Start is called before the first frame update
    void Start()
    {
        roomsData=Instantiate(Resources.Load("RoomsData", typeof(GameObject))) as GameObject;
        roomsData.GetComponent<RoomsData>().Init(MapDimension,MapDimension);
        roomsData.GetComponent<RoomsData>().setLevel(1);

        Instantiate(Resources.Load("NESW",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);
        Instantiate(Resources.Load("Floor",typeof(GameObject)),new Vector2(0,0),Quaternion.identity);



        //GameObject.FindGameObjectWithTag("RoomsData").GetComponent<RoomsData>().wait(5);
        Invoke("ChangeRooms",3f);
        Invoke("SpawnEn",3f);

    }


    void ChangeRooms()
    {
        this.roomsData.GetComponent<RoomsData>().ChangeRoom();
    }
    void SpawnEn()
    {   
        for (int x=0 ; x<roomsData.GetComponent<RoomsData>().positions.Count;x++ )
        this.roomsData.GetComponent<RoomsData>().SpawnStuffs(roomsData.GetComponent<RoomsData>().positions[x],GameObject.FindGameObjectWithTag("Templates").GetComponent<Templates>().randomFloorTile,20);
        for (int x=0 ; x<roomsData.GetComponent<RoomsData>().positions.Count;x++ )
        this.roomsData.GetComponent<RoomsData>().SpawnStuffsNoRotation(roomsData.GetComponent<RoomsData>().positions[x],GameObject.FindGameObjectWithTag("Templates").GetComponent<Templates>().enemyArray,10);
    }
}
