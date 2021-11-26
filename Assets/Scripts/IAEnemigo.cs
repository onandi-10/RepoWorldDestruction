using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
    public GameObject Target;
    public NavMeshAgent agent;

    public Health healthScript;

    public float distance;




    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            healthScript.health = healthScript.health - 10;
        }
     }



    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Target.transform.position, transform.position) < distance)
        {
            agent.SetDestination(Target.transform.position);
            agent.speed = 3;
        }
        else
        {
            agent.speed = 0;
        }

    }
}
