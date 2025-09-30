using UnityEngine;

namespace IuvoUnity
{
    namespace ProceduralGeneration
    {
        namespace Dungeons
        {
            public static class CorridorFactory
            {
                public static Corridor CreateCorridor(Vector3 worldSize, int minWidth, int maxWidth, int minHeight, int maxHeight)
                {
                    int width = Random.Range(minWidth, maxWidth);
                    int height = Random.Range(minHeight, maxHeight);
                    int length = Random.Range(10, 30); // Random corridor length

                    // Calculate a random position within the world size
                    int xPos = Random.Range(0, (int)(worldSize.x - width));
                    int zPos = Random.Range(0, (int)(worldSize.z - length));
                    Vector3 position = new Vector3(xPos, 0, zPos);

                    // Create the corridor and return it
                    return new Corridor(position, width, height, length, RandomDungeonManager.Instance.corridorPrefab);
                }
            }
        }
    }
}