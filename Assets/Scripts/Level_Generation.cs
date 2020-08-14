using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;

public class Level_Generation : MonoBehaviour
{
    public GameObject TilemapGrid;

    public  List<Tilemap> Rooms = new List<Tilemap>();
    public List<Tilemap> BossRooms = new List<Tilemap>();

    public Tilemap StartRoom;

    //connects the rooms
    public TileBase concrete;

    List<Tilemap> roomList = new List<Tilemap>();

    private void Awake()
    {
        Generate();
    }

    void Generate()
    {
        roomList = new List<Tilemap>();

        //randomly generate rooms
        float x = 0.12f;
        float y = 3f;
        float z = 0;

        //starting room
        roomList.Add(Instantiate(StartRoom, new Vector3(x, y, z), Quaternion.identity));
        roomList[0].transform.parent = TilemapGrid.transform;
        roomList[0].CompressBounds();

        x += roomList[0].size.x;

        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x - 1), (int)(roomList[0].origin.y + 7), (int)z), null);
        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x - 1), (int)(roomList[0].origin.y + 6), (int)z), null);
        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x - 1), (int)(roomList[0].origin.y + 5), (int)z), null);

        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x), (int)(roomList[0].origin.y + 8), (int)z), concrete);
        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x), (int)(roomList[0].origin.y + 8), (int)z), concrete);
        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x - 1), (int)(roomList[0].origin.y + 4), (int)z), concrete);
        roomList[0].SetTile(new Vector3Int((int)(roomList[0].origin.x + roomList[0].size.x - 2), (int)(roomList[0].origin.y + 4), (int)z), concrete);

        x += 2;

        int randomRoomNumber = Random.Range(4, 11);

        for (int i = 1; i < randomRoomNumber; i++)
        {
            roomList.Add(Instantiate(Rooms[Random.Range(0, Rooms.Count)], new Vector3(x, y, z), Quaternion.identity));
            roomList[i].transform.parent = TilemapGrid.transform;
            roomList[i].CompressBounds();

            x += roomList[i].size.x;

            //clear entrance
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x), (int)(roomList[i].origin.y + 7), (int)z), null);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x), (int)(roomList[i].origin.y + 6), (int)z), null);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x), (int)(roomList[i].origin.y + 5), (int)z), null);
            

            //connects the different rooms
            //clear the exit
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x - 1), (int)(roomList[i].origin.y + 7), (int)z), null);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x - 1), (int)(roomList[i].origin.y + 6), (int)z), null);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x - 1), (int)(roomList[i].origin.y + 5), (int)z), null);

            //walkway
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x), (int)(roomList[i].origin.y + 8), (int)z), concrete);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x), (int)(roomList[i].origin.y + 8), (int)z), concrete);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x - 1), (int)(roomList[i].origin.y + 4), (int)z), concrete);
            roomList[i].SetTile(new Vector3Int((int)(roomList[i].origin.x + roomList[i].size.x - 2), (int)(roomList[i].origin.y + 4), (int)z), concrete);

            //next room room position
            x += 2;
        }

        //boss room
        int r = randomRoomNumber;

        roomList.Add(Instantiate(BossRooms[Global.currentLevel], new Vector3(x, y, z), Quaternion.identity));
        roomList[r].transform.parent = TilemapGrid.transform;
        roomList[r].CompressBounds();

        x += roomList[randomRoomNumber].size.x;

        roomList[r].SetTile(new Vector3Int((int)(roomList[r].origin.x), (int)(roomList[r].origin.y + 7), (int)z), null);
        roomList[r].SetTile(new Vector3Int((int)(roomList[r].origin.x), (int)(roomList[r].origin.y + 6), (int)z), null);
        roomList[r].SetTile(new Vector3Int((int)(roomList[r].origin.x), (int)(roomList[r].origin.y + 5), (int)z), null);

        x += 2;
    }
}
