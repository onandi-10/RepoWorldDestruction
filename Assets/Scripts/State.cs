using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Added since we're using a navmesh.

public class State
{
    // 'States' that the NPC could be in.
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
    };

    // 'Events' - where we are in the running of a STATE.
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; // To store the name of the STATE.
    protected EVENT stage; // To store the stage the EVENT is in.
    protected GameObject npc; // To store the NPC game object.
    protected Transform player; // To store the transform of the player. This will let the guard know where the player is, so it can face the player and know whether it should be shooting or chasing (depending on the distance).
    protected State nextState; // This is NOT the enum above, it's the state that gets to run after the one currently running (so if IDLE was then going to PATROL, nextState would be PATROL).
    protected NavMeshAgent agent; // To store the NPC NavMeshAgent component.

    // Constructor for State
    public State(GameObject _npc, NavMeshAgent _agent, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        stage = EVENT.ENTER;
        player = _player;
    }

    // Phases as you go through the state.
    public virtual void Enter() { stage = EVENT.UPDATE; } // Runs first whenever you come into a state and sets the stage to whatever is next, so it will know later on in the process where it's going.
    public virtual void Update() { stage = EVENT.UPDATE; } // Once you are in UPDATE, you want to stay in UPDATE until it throws you out.
    public virtual void Exit() { stage = EVENT.EXIT; } // Uses EXIT so it knows what to run and clean up after itself.

    // The method that will get run from outside and progress the state through each of the different stages.
    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Debug.Log("Salir");
            Exit();
            return nextState; // Notice that this method returns a 'state'.
        }
        return this; // If we're not returning the nextState, then return the same state.
    }
}

// Constructor for Idle state.
public class Idle : State
{
    float wakeUpDistance = 25;

    public Idle(GameObject _npc, NavMeshAgent _agent, Transform _player)
                : base(_npc, _agent, _player)
    {
        name = STATE.IDLE; // Set name of current state.
    }

    public override void Enter()
    {
        Debug.Log("Inico Stage Idle");
        base.Enter(); // Sets stage to UPDATE.
    }

    public override void Update()
    {

        float distance = Vector3.Distance(player.position, npc.transform.position);

        if (distance < wakeUpDistance)
        {
            nextState = new Perseguir(npc, agent, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

// Constructor for Idle state.
public class Perseguir : State
{
    float wakeUpDistance = 25;

    public Perseguir(GameObject _npc, NavMeshAgent _agent, Transform _player)
                : base(_npc, _agent, _player)
    {
        name = STATE.PATROL; // Set name of current state.
    }

    public override void Enter()
    {
        base.Enter(); // Sets stage to UPDATE.
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (Vector3.Distance(player.position, npc.transform.position) > wakeUpDistance)
        {
            agent.SetDestination(npc.transform.position);
            nextState = new Idle(npc, agent, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
