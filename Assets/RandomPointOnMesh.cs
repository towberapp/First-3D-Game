
using UnityEngine;
using UnityEngine.AI;


/*
Used:
RandomPointOnNavMesh randompoint;
randompoint = gameObject.AddComponent(typeof(RandomPointOnNavMesh)) as RandomPointOnNavMesh;
randompoint.RandomPoint(transform.position, range, out point);
*/

namespace AddonScript {
public class RandomPointOnNavMesh : MonoBehaviour
{
   
    public float range = 10.0f;

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


}
}