using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HealthBar hb;
    [Header("EnemyName [player|tower]")]
    public string enemyName;
    [Header("Targets")]
    public List<Transform> AllTarget;
    public List<PlayerController> AllTargetAttr;

    [Header("Animation")]
    public Animator animator;

    public Transform Seeker;
    public AStar_Grid ag;

    private Vector3 previousPosition;
    private float movementThreshold = 0.01f;

    public Crystal crystal;

    AStar astar;
    public Enemy enemy;

    private void Start()
    {
        if (enemyName == "player") enemy = new PlayerAttacker();
        if (enemyName == "tower") enemy = new TowerAttacker();
        hb.SetMaxHealth(enemy.Health);
        previousPosition = transform.position;

        astar = gameObject.AddComponent<AStar>();
        astar.grid = ag;
        astar.assignTarget(AllTarget);
        astar.addSeek(Seeker);
        astar.Assign();
    }

    private bool hasAttacked = false;

    void loldead()
    {
        if (enemy.CheckDeath())
        {
            Destroy(gameObject);
            Battle.KillCount++;
        }
    }

    private void Update()
    {
        UpdateHealth();
        loldead();
        hasAttacked = false;
        //if (astar && ag) print(astar.RetreiveClosestTarget().name);

        Vector3 currentPosition = transform.position;
        if (!(Vector3.Distance(currentPosition, previousPosition) <= movementThreshold)) animator.SetBool("isRunning", true);


        if (Vector3.Distance(currentPosition, previousPosition) <= movementThreshold)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            animator.SetBool("isRunning", false);

            if (!(stateInfo.IsName("Attack01") && stateInfo.normalizedTime < 1f))
            {
                animator.SetTrigger("attackTrigger");
                if (enemyName == "player") Attack();
                if (enemyName == "tower") AttackTower();

            }
            previousPosition = currentPosition;
        }
        else
        {
            animator.SetBool("isRunning", true);
            previousPosition = currentPosition;
        }
    }

    private void UpdateHealth()
    {
        hb.SetHealth(enemy.Health);
    }

    private void Attack() {
        if (!hasAttacked)
        {
            Transform target = astar.RetreiveClosestTarget();
            int index = AllTarget.IndexOf(target);

            //print(enemy.BasicDamage);
            AllTargetAttr[index].currentAlly.TakeDamage(enemy.BasicDamage);

            hasAttacked = true;
        }
    }

    public void AttackTower() {
        crystal.health -= enemy.BasicDamage;
        transform.LookAt(crystal.transform);
    }



 }
