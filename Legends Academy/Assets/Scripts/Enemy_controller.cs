using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
public class Enemy_controller : MonoBehaviour
{
    public float lookRadius = 10f;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    private Animator animator;
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = Player_Manager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed;

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
        }
    }
    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }
    void OnDrawGizmosSelected (){
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
*/
public class Enemy_controller : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;

    public float distanceThreshold = 10f;

    public enum AIState {idle,chasing};

    public AIState aiState = AIState.idle;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    IEnumerator Think()
    {
        while(true)
        {

            switch (aiState)
            {
                case AIState.idle:
                float dist = Vector3.Distance(target.position, transform.position);
                if (dist < distanceThreshold)
                {
                    aiState = AIState.chasing;
                }
                agent.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                dist = Vector3.Distance(target.position, transform.position);
                if (dist > distanceThreshold)
                {
                    aiState = AIState.idle;
                }
                agent.SetDestination(target.position);
                    break;
                default:
                    break;
            }

            //agent.SetDestination(target.position);
            yield return new WaitForSeconds(1f);
        }
    }
}