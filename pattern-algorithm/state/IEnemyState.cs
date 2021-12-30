using UnityEngine;

public interface IEnemyState
{
    void Start(EnemyStateController enemyController);
    IEnemyState Update();
}

public class IdleState : IEnemyState
{
    EnemyStateController enemyController;
    RaycastHit hit;
    float distance = 5.0f;

    public void Start(EnemyStateController enemyController)
    {
        this.enemyController = enemyController;
    }

    public IEnemyState Update()
    {
        // check if the Player was seen, via raycast for example
        Ray ray = new Ray(enemyController.transform.position, enemyController.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return new WalkState();
            }
        }

        return null;
    }
}

public class WalkState : IEnemyState
{
    EnemyStateController enemyController;

    public void Start(EnemyStateController enemyController)
    {
        this.enemyController = enemyController;
    }

    public IEnemyState Update()
    {
        // TODO: get access to the Player position
        enemyController.NavMeshAgent.SetDestination(GameManager.Instance.Player.transform.position);
        // in this case, I'm using the Blend Tree to control the animations of the Enemy
        enemyController.Animator.SetFloat("Speed", enemyController.NavMeshAgent.velocity.magnitude);

        // if the distance of the Enemy and the Player is less than the radius + stoppingDistance of the NavMeshAgent, start the attack!
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, enemyController.transform.position) <=
            (enemyController.NavMeshAgent.stoppingDistance + enemyController.NavMeshAgent.radius))
        {
            return new AttackState();
        }

        return null;
    }
}

public class AttackState : IEnemyState
{
    EnemyStateController enemyController;
    float rotationSpeed = 5.0f;

    public void Start(EnemyStateController enemyController)
    {
        this.enemyController = enemyController;
        this.enemyController.Animator.SetTrigger("Attack");
    }

    public IEnemyState Update()
    {
        // to face the Player smoothly on attack:

        // TODO: get access to the Player position
        Vector3 attackDirection = (GameManager.Instance.Player.transform.position - enemyController.transform.position).normalized;
        // in this case, we don't want to affect the Y rotation, we only care about the direction from X and Z
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(attackDirection.x, 0, attackDirection.z));
        enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // in this case, I'm using the Blend Tree to control the animations of the Enemy. Check if the animation is NOT the Attack animation.
        if (enemyController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
        {
            return new WalkState();
        }

        return null;
    }
}