using IuvoUnity.Extensions;
using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoProceduralGeneration
    {
        namespace Dungeons
        {

            public class Corridor
            {
                public Vector3 startPosition;
                public int width, height, length;

                public Corridor(Vector3 position, int corridorWidth, int corridorHeight, int corridorLength, GameObject corridorPrefab)
                {
                    if (position.HasObjectWithLayerInBox(LayerMask.NameToLayer("PG_Corridor"), new Vector3(corridorWidth, corridorHeight, corridorLength)))
                    {
                        return;
                    }

                    //if (position.HasObjectWithLayerInBox(LayerMask.NameToLayer("PG_Floor"), new Vector3(corridorWidth, corridorHeight, corridorLength)))
                    //{
                    //    return;
                    //}

                    startPosition = position;
                    width = corridorWidth;
                    height = corridorHeight;
                    length = corridorLength;


                    GameObject corridorFloor = Object.Instantiate(corridorPrefab, startPosition, Quaternion.identity);
                    corridorFloor.transform.MultiplyLocalScale(new Vector3(width, height, length));

                    corridorFloor.layer = LayerMask.NameToLayer("PG_Corridor");

                }

            }
        }
    }
}