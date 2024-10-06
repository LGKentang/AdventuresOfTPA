using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingLogic : MonoBehaviour
{
    //[Header("Entities")]
    //public List<Transform> allies;
    //public List<Transform> enemies;
    //public List<PlayerController> allyController;

    //[Header("Pathfinding Logic")]
    //AStar astar_for_enemy;
    ////AStar astar_for_allies;
    //public AStar_Grid grid;

    AStar astar;
    AStar bstar;
    public List<Transform> targets;
    public List<Transform> seekers;

    private void Awake()
    {
        astar = gameObject.AddComponent<AStar>();
        astar.assignTarget(targets);

        //bstar = gameObject.AddComponent<AStar>();
        //bstar.assignTarget(seekers);

        foreach (Transform t in seekers)
        {
            astar.addSeek(t);
        }

        //foreach (Transform t in targets)
        //{
        //    bstar.addSeek(t);
        //}

        astar.Assign();
        //bstar.Assign();
    }

    void Start()
    {

    }

    void Update()
    {
        //astar_for_enemy = gameObject.AddComponent<AStar>();
        //astar_for_allies = gameObject.AddComponent<AStar>();

        //astar_for_enemy.assignTarget(allies);
        //astar_for_allies.assignTarget(enemies);

        //foreach (Transform enemy in enemies)
        //{
        //    astar_for_enemy.addSeek(enemy);
        //}

        //foreach (Transform ally in allies)
        //{
        //    astar_for_allies.addSeek(ally);
        //}


        //for (int i = 0; i < allyController.Count; i++)
        //{
        //    if (allyController[i].IsBeingControlled == false)
        //    {
        //        astar_for_allies.addSeek(allies[i]);
        //    }
        //}

        //astar_for_enemy.Assign();
        //astar_for_allies.Assign();

        //for (int i = 0; i < allyController.Count; i++)
        //{
        //    if (allyController[i].IsBeingControlled == false)
        //    {
        //        astar_for_allies.addSeek(allies[i]);
        //    }
        //}

        //astar_for_enemy.Assign();
    }
}
