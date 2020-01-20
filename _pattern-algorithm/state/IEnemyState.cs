using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    void Start(EnemyControllerState enemyController);

    IEnemyState Update();
}

public class IdleState : IEnemyState {

    EnemyControllerState _enemyController;
    RaycastHit _hit;
    float _distance = 5.0f;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
    }

    public IEnemyState Update() {
        // check if the Player was seen, via raycast for example
        Ray ray = new Ray(_enemyController.transform.position, _enemyController.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance, Color.yellow);
        if (Physics.Raycast(ray, out _hit, _distance)) {
            if (_hit.transform.CompareTag("Player")) {
                return new WalkState();
            }
        }
        return null;
    }
}

public class WalkState : IEnemyState {

    EnemyControllerState _enemyController;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
    }

    public IEnemyState Update() {
        // TODO: get access to the Player position
        _enemyController.NavMeshAgent.SetDestination(GameManager.Instance.Player.transform.position);
        // in this case, I'm using the Blend Tree to control the animations of the Enemy
        _enemyController.Animator.SetFloat("Speed", _enemyController.NavMeshAgent.velocity.magnitude);

        // if the distance of the Enemy and the Player is less than the radius + stoppingDistance of the NavMeshAgent, start the attack!
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, _enemyController.transform.position) <= (_enemyController.NavMeshAgent.stoppingDistance + _enemyController.NavMeshAgent.radius)) {
            return new AttackState();
        }

        return null;
    }
}

public class AttackState : IEnemyState {

    EnemyControllerState _enemyController;
    float _rotationSpeed = 5.0f;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
        _enemyController.Animator.SetTrigger("Attack");
    }

    public IEnemyState Update() {
        // to face the Player smoothly on attack:

        // TODO: get access to the Player position
        Vector3 attackDirection = (GameManager.Instance.Player.transform.position - _skeletonController.transform.position).normalized;
        // in this case, we don't want to affect the Y rotation, we only care about the direction from X and Z
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(attackDirection.x, 0, attackDirection.z));
        _skeletonController.transform.rotation = Quaternion.Slerp(_skeletonController.transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

        // in this case, I'm using the Blend Tree to control the animations of the Enemy. Check if the animation is NOT the Attack animation.
        if (_enemyController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree")) {
            return new WalkState();
        }

        return null;
    }
}