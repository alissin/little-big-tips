using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    IEnemyState state;

    Animator animator;

    public Animator Animator
    {
        get => animator;
    }

    NavMeshAgent navMeshAgent;

    public NavMeshAgent NavMeshAgent
    {
        get => navMeshAgent;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        state = new IdleState();
        state.Start(this);
    }

    void Update()
    {
        IEnemyState newState = state.Update();
        if (newState != null)
        {
            state = newState;
            state.Start(this);
        }
    }
}