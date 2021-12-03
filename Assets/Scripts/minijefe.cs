using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minijefe : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    State actualState;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        actualState = new Idle(gameObject, agent, player);
    }

    // Update is called once per frame
    void Update()
    {
        actualState = actualState.Process();

    }
}
