using UnityEngine;
using System.Collections.Generic;
using IuvoUnity.Debug;


namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        public class Room : MonoBehaviour
        {
            [SerializeField] public BoxCollider roomBounds;
            [SerializeField] public Vector3 center;
            [SerializeField] public GameObject roomFloor;
            [SerializeField] public Wall northWall, eastWall, southWall, westWall;
            public List<Wall> walls = new List<Wall>();
            [SerializeField] public Transform Top_Middle, Bottom_Middle, Left_Middle, Right_Middle;
            public int boundaryWalls = 0;
            public bool IsBoundaryRoom { get; private set; } = false;
            public bool IsNextToBoundaryRoom { get; private set; }


            public void Start()
            {
                if (roomFloor == null)
                {
                    IuvoDebug.DebugLogError("Room floor prefab is null. Cannot create room.");
                    return;
                }

                roomBounds = GetComponent<BoxCollider>();
                if (roomBounds == null)
                {
                    IuvoDebug.DebugLogError("BoxCollider is missing on the Room GameObject.");
                    return;
                }
                center = roomBounds.center;
                //CheckBoundaryWalls();
                northWall.myNumber = 1;
                eastWall.myNumber = 2;
                southWall.myNumber = 3;
                westWall.myNumber = 4;



                // Assigning this room to each of the present room's walls
                // North Wall
                if (northWall != null)
                {
                    northWall.myRoom = this;
                    walls.Add(northWall);
                }
                else
                {
                    IuvoDebug.DebugLogError("Room is missing North Wall.");
                }
                //South Wall
                if (southWall != null)
                {

                    southWall.myRoom = this;
                    walls.Add(southWall);
                }
                else
                {
                    IuvoDebug.DebugLogError("Room is missing South Wall.");
                }
                //East Wall
                if (eastWall != null)
                {
                    eastWall.myRoom = this;
                    walls.Add(eastWall);
                }
                else
                {
                    IuvoDebug.DebugLogError("Room is missing East Wall.");
                }
                //West Wall
                if (westWall != null)
                {
                    westWall.myRoom = this;
                    walls.Add(westWall);
                }
                else
                {
                    IuvoDebug.DebugLogError("Room is missing West Wall.");
                }

                // Counts the number of walls that identify as boundaries
                // if that number is greater than zero the room is
                // marked as a boundary room as well
                CountBoundaryWalls();

                for (int i = 0; i < walls.Count; i++)
                {
                    if (walls[i].iAmBoundaryWall)
                    {
                        for (int j = 0; j < walls[i].myContacts.Count; j++)
                        {
                            if (walls[i].myContacts[j].transform == walls[i].transform)
                            {
                                var wall = walls[i].myContacts[j].GetComponent<Wall>();
                                if (wall != null)
                                { wall.Destroy(); }
                            }
                        }

                        walls.RemoveAt(i);
                    }
                }
            }


            public Room(Vector3 position, int width, int height, int length, GameObject roomPrefab)
            {
                GameObject roomObject = Object.Instantiate(roomPrefab, position, Quaternion.identity);
                Room room = roomObject.GetComponent<Room>();
                room.roomBounds.size = new Vector3(width, height, length);
            }



            /// <summary>
            /// Sets up the room dimensions and other properties.
            /// </summary>
            /// <param name="size">The size of the room.</param>
            public void InitializeRoom(Vector3 size, bool isBoundaryRoom)
            {
                if (roomBounds == null)
                {
                    IuvoDebug.DebugLogError("BoxCollider is missing on the Room GameObject.");
                    return;
                }

                roomBounds.size = size;
                center = roomBounds.center;
                IsBoundaryRoom = isBoundaryRoom;
            }



            /// <summary>
            /// Factory method to create a Room instance.
            /// </summary>
            /// <param name="position">The position of the room.</param>
            /// <param name="size">The size of the room.</param>
            /// <param name="roomPrefab">The room prefab.</param>
            /// <param name="isBoundaryRoom">Indicates if this room is a boundary room.</param>
            /// <returns>A new Room instance.</returns>
            public static Room CreateRoom(Vector3 position, Vector3 size, GameObject roomPrefab, bool isBoundaryRoom)
            {
                if (roomPrefab == null)
                {
                    IuvoDebug.DebugLogError("Room prefab is null.");
                    return null;
                }

                GameObject roomObject = Instantiate(roomPrefab, position, Quaternion.identity);
                Room room = roomObject.GetComponent<Room>();

                if (room == null)
                {
                    IuvoDebug.DebugLogError("The provided prefab does not have a Room component.");
                    Destroy(roomObject);
                    return null;
                }

                room.InitializeRoom(size, isBoundaryRoom);
                return room;
            }

            public void DisableWall(int wallID)
            {
                if (wallID == 16)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }

                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }

                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }

                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 1)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }
                }
                else if (wallID == 2)
                {
                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }
                }
                else if (wallID == 3)
                {
                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }
                }
                else if (wallID == 4)
                {

                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 5)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }



                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 6)
                {
                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }


                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }
                }
                else if (wallID == 7)
                {
                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }



                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 8)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }



                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }

                }
                else if (wallID == 9)
                {
                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }



                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 10)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }


                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }
                }
                else if (wallID == 11)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }

                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }

                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }


                }
                else if (wallID == 12)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }


                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }


                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 13)
                {
                    if (southWall != null && !southWall.iAmBoundaryWall)
                    {
                        if (southWall.siblingWall != null)
                        {
                            Destroy(southWall.siblingWall);
                        }
                        southWall.Destroy();
                    }

                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }



                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }
                }
                else if (wallID == 14)
                {
                    if (northWall != null && !northWall.iAmBoundaryWall)
                    {
                        if (northWall.siblingWall != null)
                        {
                            Destroy(northWall.siblingWall);
                        }
                        northWall.Destroy();
                    }



                    if (westWall != null && !westWall.iAmBoundaryWall)
                    {
                        if (westWall.siblingWall != null)
                        {
                            Destroy(westWall.siblingWall);
                        }
                        westWall.Destroy();
                    }


                    if (eastWall != null && !eastWall.iAmBoundaryWall)
                    {
                        if (eastWall.siblingWall != null)
                        {
                            Destroy(eastWall.siblingWall);
                        }
                        eastWall.Destroy();
                    }


                }
                else if (wallID == 15)
                {
                    if (roomFloor != null)
                    {
                        roomFloor.gameObject.gameObject.SetActive(false);
                    }
                }
                else { return; }


            }

            public void CountBoundaryWalls()
            {
                int boundaryWalls = 0;

                if (northWall.iAmBoundaryWall)
                {
                    boundaryWalls++;
                }

                if (southWall.iAmBoundaryWall)
                {

                    boundaryWalls++;

                }

                if (westWall.iAmBoundaryWall)
                {
                    boundaryWalls++;

                }
                if (eastWall.iAmBoundaryWall)
                {
                    boundaryWalls++;
                }

                if (boundaryWalls != 0)
                {
                    IsBoundaryRoom = true;
                }



            }

            public void RemoveSharedWalls(List<Room> allRooms)
            {
                foreach (Wall wall in walls)
                {
                    Collider wallCollider = wall.GetComponent<Collider>();
                    if (wallCollider == null)
                    {
                        IuvoDebug.DebugLogError($"Wall {wall.name} has no collider. Skipping.");
                        continue;
                    }

                    // Calculate raycast length based on wall dimensions
                    float rayLength = wallCollider.bounds.size.z / 2f + 0.1f; // Add a small buffer to ensure overlap detection

                    RaycastHit hit;
                    if (Physics.Raycast(wall.transform.position, -wall.transform.forward, out hit, rayLength))
                    {
                        // Check if the ray hit another room
                        Room neighboringRoom = hit.transform.GetComponentInParent<Room>();
                        if (neighboringRoom != null && neighboringRoom != this)
                        {
                            // Remove the wall in both rooms
                            wall.gameObject.SetActive(false);
                            Wall correspondingWall = neighboringRoom.GetWallFacingPosition(wall.transform.position);
                            if (correspondingWall != null)
                            {
                                correspondingWall.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }

            public Wall GetWallFacingPosition(Vector3 position)
            {
                foreach (Wall wall in walls)
                {
                    // Check if the wall faces the given position
                    if (Vector3.Dot(wall.transform.forward, position - wall.transform.position) > 0)
                    {
                        return wall;
                    }
                }
                return null;
            }


        }

    }
}