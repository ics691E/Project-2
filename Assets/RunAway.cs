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
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            if (!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + " needs a navmesh agent");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = player.position - transform.position;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            return;
        }
        Vector3 normDir = (player.position - transform.position).normalized;
        normDir = Quaternion.AngleAxis(Random.Range(0, 179), Vector3.up) * normDir;
        MoveToPos(transform.position - (normDir * displacementDist));
    }

    private void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
