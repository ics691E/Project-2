using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class RunAway : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform chaser = null;
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

    // Update is called once per frame
    void Update()
    {
        if (chaser == null) {
            return;
        }
        Vector3 normdir = (chaser.position - transform.position).normalized;
        MoveToPos(transform.position + (normdir * displacementDist));
    }

    private void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
