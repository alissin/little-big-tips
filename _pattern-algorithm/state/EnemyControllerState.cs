using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerState : MonoBehaviour {

    IEnemyState _state;

    Animator _animator;
    public Animator Animator {
        get => _animator;
    }
    NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent {
        get => _navMeshAgent;
    }

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _state = new IdleState();
        _state.Start(this);
    }

    // Update is called once per frame
    void Update() {
        IEnemyState newState = _state.Update();
        if (newState != null) {
            _state = newState;
            _state.Start(this);
        }
    }
}
