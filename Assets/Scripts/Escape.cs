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
    [SerializeField] private float aggroRange = 3;

    private Animator animator = null;
    
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
        
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        Vector3 dir = (player.position - transform.position).normalized;
        
        float distance = Mathf.Pow(transform.position.x - player.position.x, 2) + 
                        Mathf.Pow(transform.position.z - player.position.z, 2);
        

        if (distance <= Mathf.Pow(aggroRange, 2)) {
            MoveToPos(transform.position - dir * displacementDist);
            animator.SetBool("isRunning", true);
        } else if (agent.remainingDistance <= 0.1f) {
            animator.SetBool("isRunning", false);
            agent.isStopped = true;
        }
    }

    private void MoveToPos(Vector3 pos) {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
