using System.Collections.Generic;
using UnityEngine;
using IuvoUnity.ProceduralGeneration.Dungeons;

namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        public class Wall : MonoBehaviour
        {
            public bool iAmBoundaryWall = false;
            public List<Collider> myContacts = new List<Collider>();
            public int myNumber = 0;

            public Room myRoom { get; set; }
            public Wall siblingWall { get; set; }



            public void Start()
            {
            }

            public bool IsBoundary()
            {
                BoxCollider coll = GetComponent<BoxCollider>();
                int layer = LayerMask.NameToLayer("PG_Wall");

                if (coll != null)
                {
                    Collider[] colliders = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, Quaternion.identity, 1 << layer);

                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i] != coll)
                        {
                            myContacts.Add(colliders[i]);
                        }
                    }

                    if (myContacts.Count > 6)
                    {
                        iAmBoundaryWall = false;
                    }
                    else
                    {
                        iAmBoundaryWall = true;
                    }

                    for (int i = 0; i < myContacts.Count; i++)
                    {
                        if (myContacts[i] != null && myContacts[i].transform == transform)
                        {
                            Wall theirWall = myContacts[i].GetComponent<Wall>();
                            if (theirWall != null)
                            {
                                siblingWall = theirWall;
                            }
                        }
                    }

                }
                return iAmBoundaryWall;
            }

            public void Destroy()
            {
                if (!iAmBoundaryWall)
                {
                    GridDungeonManager.Instance.walls.Remove(this);
                    Object.Destroy(this);
                }
            }




        }
    }
}