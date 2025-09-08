using System;
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoProceduralGeneration
    {
        namespace Dungeons
        {
            public class RandomDungeonManager : MonoBehaviour
            {
                public static RandomDungeonManager Instance { get; private set; } // Singleton instance 
                public bool generate = false;
                public bool isFinished = false;

                [Header("Random World Parameters")]
                [Range(5, 100)] public int randMinWorldWidth = 25;
                [Range(5, 200)] public int randMaxWorldWidth = 100;
                [Space(3)]
                [Range(5, 100)] public int randMinWorldLength = 20;
                [Range(5, 200)] public int randMaxWorldLength = 100;
                [Space(3)]
                [Range(1, 10)] public int randMinWorldHeight = 1;
                [Range(11, 100)] public int randMaxWorldHeight = 3;
                [Space(3)]
                [Range(5, 100)] public int randMaxNumOfRooms = 25;
                [Space(10)]

                [Header("Random Room Parameters")]
                [Range(1, 50)] public int randMinRoomWidth = 5;
                [Range(1, 50)] public int randMaxRoomWidth = 15;
                [Space(3)]
                [Range(1, 50)] public int randMinRoomHeight = 5;
                [Range(1, 50)] public int randMaxRoomHeight = 15;
                [Space(3)]
                [Range(1, 50)] public int randMinRoomLength = 5;
                [Range(1, 50)] public int randMaxRoomLength = 15;
                [Space(3)]
                [Range(1, 25)] public int randMinNumberOfRooms = 3;
                [Range(1, 500)] public int randMaxNumberOfRooms = 10;
                [Space(10)]



                [Header("Prefabs")]
                public GameObject roomFloorPrefab; // Room floor prefab (thin cube)
                public GameObject corridorPrefab; // Corridor wall prefab (two walls, cieling and floor)
                [Space(10)]

                [Header("Collections")]
                public List<Room> rooms = new List<Room>();
                public List<Corridor> corridors = new List<Corridor>();

                private Vector3 currentWorldSize = Vector3.zero;




                private void Awake()
                {
                    if (Instance == null)
                    {
                        Instance = this;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }


                public void Update()
                {
                    if (!isFinished)
                    {

                        if (rooms.Count >= randMaxNumOfRooms)
                        {
                            isFinished = true;
                            return;
                        }
                        //GenerateRandomDungeon();


                    }
                    else
                    {
                        // 
                    }
                }



                //void GenerateRandomRoom(Vector3 worldSize)
                //{
                //    Room room = RoomFactory.CreateRandomRoom(worldSize, randMinRoomWidth, randMaxRoomWidth, randMinRoomHeight, randMaxRoomHeight, randMinRoomLength, randMaxRoomLength);
                //    if (room != null)
                //    {
                //        rooms.Add(room);
                //    }
                //}

                //public void GenerateRandomRooms()
                //{
                //    int worldWidth = UnityEngine.Random.Range(randMinWorldWidth, randMaxWorldWidth);
                //    int worldLength = UnityEngine.Random.Range(randMinWorldLength, randMaxWorldLength);
                //    int worldHeight = UnityEngine.Random.Range(randMinWorldHeight, randMaxWorldHeight);
                //    Vector3 worldSize = new Vector3(worldWidth, worldHeight, worldLength);
                //    currentWorldSize = worldSize;
                //    int numberOfRooms = worldWidth * worldLength;
                //    for (int i = 0; i < numberOfRooms; i++)
                //    {
                //        GenerateRandomRoom(currentWorldSize);
                //    }

            }

            //    public void GenerateRandomDungeon()
            //    {
            //        int worldWidth = UnityEngine.Random.Range(randMinWorldWidth, randMaxWorldWidth);
            //        int worldLength = UnityEngine.Random.Range(randMinWorldLength, randMaxWorldLength);
            //        int worldHeight = UnityEngine.Random.Range(randMinWorldHeight, randMaxWorldHeight);

            //        Vector3 worldSize = new Vector3(worldWidth, worldHeight, worldLength);
            //        currentWorldSize = worldSize;
            //        int numberOfRooms = UnityEngine.Random.Range(randMinNumberOfRooms, randMaxNumberOfRooms);
            //        for (int i = 0; i < numberOfRooms; i++)
            //        {
            //            GenerateRandomRoom(currentWorldSize);
            //        }
            //    }




            //}


        }
    }
}