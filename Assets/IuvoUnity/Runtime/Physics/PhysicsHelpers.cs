using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoPhysics
    {
        public static class PhysicsHelpers
        {
            /// <summary>
            /// Does not respect debugging flags. Should call the IuvoDebug Version for debugging flags
            /// </summary>
            public static bool RaycastDebug(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, Color? debugColor = null)
            {
                //return IuvoDebug.RaycastDebug(origin, direction, out hit, distance, layerMask, debugColor);

                bool result = Physics.Raycast(origin, direction, out hit, distance, layerMask);
                UnityEngine.Debug.DrawRay(origin, direction.normalized * distance, debugColor ?? (result ? Color.green : Color.red));
                return result;
            }


            public static bool HasLineOfSight(Vector3 from, Vector3 to, LayerMask mask)
            {
                Vector3 direction = to - from;
                return !Physics.Raycast(from, direction.normalized, direction.magnitude, mask);
            }


            public static GameObject GetObjectUnderMouse(Camera cam, LayerMask mask, string requiredTag = null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
                {
                    if (string.IsNullOrEmpty(requiredTag) || hit.collider.CompareTag(requiredTag))
                        return hit.collider.gameObject;
                }
                return null;
            }

            public static float DistanceToGround(Transform obj, float maxDistance = 10f, LayerMask groundMask = default)
            {
                if (Physics.Raycast(obj.position, Vector3.down, out RaycastHit hit, maxDistance, groundMask))
                    return hit.distance;
                return -1f; // not grounded
            }

            public static bool RaycastClosest(Vector3 origin, Vector3 direction, out RaycastHit closestHit, float distance = 100f, LayerMask mask = default)
            {
                RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, mask);
                if (hits.Length > 0)
                {
                    closestHit = hits.OrderBy(h => h.distance).First();
                    return true;
                }
                closestHit = new RaycastHit();
                return false;
            }

            public static bool InVisionCone(Transform observer, Transform target, float viewAngle, float viewDistance, LayerMask losMask)
            {
                Vector3 dirToTarget = (target.position - observer.position).normalized;
                if (Vector3.Angle(observer.forward, dirToTarget) < viewAngle / 2f)
                {
                    float dist = Vector3.Distance(observer.position, target.position);
                    if (!Physics.Raycast(observer.position, dirToTarget, dist, losMask))
                    {
                        return dist <= viewDistance;
                    }
                }
                return false;
            }




            public static bool IsNextToWall(Transform transform, float checkDistance, LayerMask wallMask, out Vector3 wallNormal)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.right, out hit, checkDistance, wallMask))
                {
                    wallNormal = hit.normal;
                    return true;
                }
                if (Physics.Raycast(transform.position, -transform.right, out hit, checkDistance, wallMask))
                {
                    wallNormal = hit.normal;
                    return true;
                }
                wallNormal = Vector3.zero;
                return false;
            }

            public static bool HasCoverFrom(Vector3 coverSeekerPos, Vector3 threatPos, LayerMask wallMask)
            {
                Vector3 dir = threatPos - coverSeekerPos;
                return Physics.Raycast(coverSeekerPos, dir.normalized, out _, dir.magnitude, wallMask);
            }

            public static List<Vector2Int> GetNeighbours4(this Vector2Int tile)
            {
                return new List<Vector2Int> {
                    tile + Vector2Int.up,
                    tile + Vector2Int.down,
                    tile + Vector2Int.left,
                    tile + Vector2Int.right
                };
            }

            public static List<Vector2Int> GetNeighbours8(this Vector2Int tile)
            {
                var list = tile.GetNeighbours4();
                list.Add(tile + new Vector2Int(-1, -1));
                list.Add(tile + new Vector2Int(1, -1));
                list.Add(tile + new Vector2Int(-1, 1));
                list.Add(tile + new Vector2Int(1, 1));
                return list;
            }

            public static List<Vector3Int> GetNeighbours6(this Vector3Int cell)
            {
                return new List<Vector3Int>
                {
                    cell + Vector3Int.up,
                    cell + Vector3Int.down,
                    cell + Vector3Int.left,
                    cell + Vector3Int.right,
                    cell + Vector3Int.forward,
                    cell + Vector3Int.back
                };
            }

            public static List<Vector3Int> GetNeighbours26(this Vector3Int cell)
            {
                List<Vector3Int> neighbours = new List<Vector3Int>();
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                        for (int z = -1; z <= 1; z++)
                            if (x != 0 || y != 0 || z != 0)
                                neighbours.Add(cell + new Vector3Int(x, y, z));
                return neighbours;
            }





        }

    }
}