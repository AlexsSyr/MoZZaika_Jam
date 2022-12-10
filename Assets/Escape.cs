using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escape : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform player = null;
    [SerializeField] private float displacementDist = 1;
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        
        Vector3 direction = (-transform.position + player.position).normalized;

        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
    void Start()
    {
        if (agent == null)
            if (!TryGetComponent(out agent))
                Debug.LogWarning(name + " needs a navmesh agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        Vector3 dir = (player.position - transform.position).normalized;

        MoveToPos(transform.position - dir * displacementDist);
    }

    private void MoveToPos(Vector3 pos) {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
