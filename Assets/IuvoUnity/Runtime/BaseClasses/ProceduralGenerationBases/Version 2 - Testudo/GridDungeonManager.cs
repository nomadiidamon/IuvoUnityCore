using IuvoUnity._Extensions;
using IuvoUnity.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridDungeonManager : MonoBehaviour
{
    public static GridDungeonManager Instance { get; private set; } // Singleton instance
    public bool generate = false;
    public bool changedBoundaryColor = false;
    [Header("Wall Percentage")]
    [Range(0, 1)] public float wallPerctentage = 1f;
    [Space(10)]


    public bool isFinished = false;
    public bool isFinishedMakingRooms = false;
    public bool dungeonIsPathable = false;

    [Header("Test Dummy")]
    public GameObject tester;
    [Header("Test Strength")]
    [Range(0, 1)] float testStrength = 1f;


    [Header("Prefabs")]
    public GameObject roomFloorPrefab;
    //public GameObject roomDoorPrefab;
    [Space(10)]

    [Header("World Parameters")]
    [Range(1, 500)] public int WorldWidth = 100;
    [Range(1, 500)] public int WorldHeight = 5;
    [Range(1, 500)] public int WorldLength = 100;
    public int NumOfRooms = 0;
    [Space(10)]

    [Header("Room Parameters")]
    [Range(1, 50)] public int RoomWidth = 15;
    [Range(1, 50)] public int RoomHeight = 15;
    [Range(1, 50)] public int RoomLength = 15;

    [Header("Room Distance")]
    [Range(0, 5)] public float distanceFactor = 0.5f;




    [Header("Collections")]
    public List<Room> rooms = new List<Room>();
    public List<Wall> walls = new List<Wall>();
    public List<Wall> boundaryWalls = new List<Wall>();

    private Vector3 currentWorldSize = Vector3.zero;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        NumOfRooms = WorldWidth * WorldLength;

    }

    void Update()
    {
        if (!isFinished)
        {
            if (rooms.Count >= NumOfRooms)
            {
                isFinishedMakingRooms = true;
            }
            else
            {
                GenerateGridDungeon();
                SetWorldBoundaries();
            }

            if (isFinishedMakingRooms)
            {
       
                RemoveWalls();
                RemoveWalls();
                //RemoveWalls();
            }




        }
    }


    public void GenerateGridDungeon()
    {
        GenerateGridOfRooms();
    }

    void GenerateGridRoom(Vector3 position, int width, int height, int length)
    {
        GameObject roomObject = Instantiate(roomFloorPrefab, position, Quaternion.identity);
        Room room = roomObject.GetComponent<Room>();
        if (room != null)
        {
            room.roomBounds.size = new Vector3(width, height, length);
            rooms.Add(room);
            walls.Add(room.northWall);
            walls.Add(room.eastWall);
            walls.Add(room.southWall);
            walls.Add(room.westWall);
        }
    }

    void GenerateGridOfRooms()
    {
        currentWorldSize = transform.position;

        for (int h = 0; h < WorldHeight; h++)
        {

            for (int i = 0; i < WorldWidth; i++)
            {

                for (int j = 0; j < WorldLength; j++)
                {
                    if (currentWorldSize.HasObjectWithLayerInBox(LayerMask.NameToLayer("PG_Room"), new Vector3(RoomLength / 2, RoomWidth / 2, RoomHeight / 2)))
                    {
                        continue;
                    }
                    GenerateGridRoom(currentWorldSize, RoomWidth, RoomHeight, RoomLength);
                    currentWorldSize = currentWorldSize.Add(z: 1 * distanceFactor);

                }
                currentWorldSize = currentWorldSize.WithZ(transform.position.z);
                currentWorldSize = currentWorldSize.Add(x: 1 * distanceFactor);
            }
            currentWorldSize = currentWorldSize.WithX(transform.position.x);
            currentWorldSize = currentWorldSize.Add(y: 1 * distanceFactor);

        }

    }

    private void SetWorldBoundaries()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].IsBoundary();
        }

        for (int i = 0; i < walls.Count; i++)
        {
            if (walls[i].iAmBoundaryWall)
            {
                boundaryWalls.Add(walls[i]);

                if (changedBoundaryColor)
                {
                    MeshRenderer rend = walls[i].GetComponent<MeshRenderer>();
                    if (rend != null)
                    {
                        walls[i].gameObject.SetMaterialColor(Color.white);
                    }
                }

            }
        }
    }

    public void RemoveWalls()
    {


        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    rooms[i].CountBoundaryWalls();

        //    rooms[i].RemoveSharedWalls(rooms);


        //    if (i % RoomWidth == 0)
        //    {
        //        int side = Random.Range(1, 4);

        //        switch (side)
        //        {
        //            case 1:
        //                rooms[i].northWall.gameObject.SetActive(false);
        //                break;

        //            case 2:
        //                rooms[i].eastWall.gameObject.SetActive(false);
        //                break;

        //            case 3:
        //                rooms[i].southWall.gameObject.SetActive(false);
        //                break;

        //            case 4:
        //                rooms[i].westWall.gameObject.SetActive(false);
        //                break;

        //            default:
        //                break;




        //        }
        //    }


        //}
        //randomly deactivate walls
        //int randomNum = UnityEngine.Random.Range(0, 15);
        //int max = rooms.Count;
        //for (int i = max; i < (max % (randomNum +1)); i--)
        //{
        //    int newRandomNum = UnityEngine.Random.Range(0, 15);
        //    rooms[i].DisableWall(newRandomNum);

        //}

        for (int i = 0; i < walls.Count; i++)
        {
            for (int j = 0; j < boundaryWalls.Count; j++)
            {
                if (walls[i] == boundaryWalls[j])
                {
                    walls.RemoveAt(i);
                }
            }
        }

        

        for (int i = 0; i < walls.Count; i++)
        {

            if (walls[i].myRoom.IsBoundaryRoom)
            {
                continue;
            }
            else
            {
                int randomNumForWallOperation = Random.Range(1, 16);
                walls[i].myRoom.DisableWall(randomNumForWallOperation);
            }

        }

    }


}
