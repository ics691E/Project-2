using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class RunAway : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform player = null; 
    [SerializeField] private float displacementDist = 5f;
    [SerializeField] private float checkDistance = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null && !TryGetComponent(out agent))
        {
            Debug.LogWarning($"{name} needs a navmesh agent component.");
        }
    }
    
    // For Debugging
    // private void OnDrawGizmos()
    // {
    //    Gizmos.color = Color.red;
    //    Vector3 direction = player.position - transform.position;
    //    Gizmos.DrawLine(transform.position, transform.position + direction);
    //}

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 escapeDirection = CalculateBestEscapeDirection();
        Vector3 newPosition = transform.position + escapeDirection * displacementDist;
        agent.SetDestination(newPosition);
    }

    private Vector3 CalculateBestEscapeDirection()
    {
        Vector3 bestDirection = Vector3.zero;
        float maxDistance = 0f;

        // Check in multiple directions around the AI
        for (int angle = 0; angle < 360; angle += 30)
        {
            Vector3 testDirection = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            Vector3 testPosition = transform.position + testDirection * checkDistance; // Use checkDistance to project the test position

            if (NavMesh.SamplePosition(testPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(hit.position, path);
                if (path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 1)
                {
                    // Calculate path length
                    float pathLength = PathLength(path);
                    if (pathLength > maxDistance && pathLength >= checkDistance) // Ensure path is sufficiently long
                    {
                        maxDistance = pathLength;
                        bestDirection = testDirection;
                    }
                }
            }
        }

        return bestDirection == Vector3.zero ? transform.forward : bestDirection; // Fallback to forward if no valid direction is found
    }

    private float PathLength(NavMeshPath path)
    {
        float length = 0.0f;
        for (int i = 1; i < path.corners.Length; i++)
        {
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        return length;
    }
}
