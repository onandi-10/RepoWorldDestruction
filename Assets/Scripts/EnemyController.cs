using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if(!chasing)
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;
            }

            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if(chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
        }else
        {
            //transform.LookAt(targetPoint);

            //TheRB.velocity = transform.forward * moveSpeed;

            agent.destination = targetPoint;

            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;

                chaseCounter = keepChasingTime;
            }
        }
        
        
    }
}
