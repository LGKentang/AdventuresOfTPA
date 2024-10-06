using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvGenerator : MonoBehaviour
{

    [Header("Crystal")]
    public GameObject crystal;

    //[Header("Enemy")]
    //public GameObject enemyPrefab;

    [Header("Objects Prefab")]
    public List<GameObject> objects;

    Prim pr;
    [Header("Prim")]
    public int r;
    public int c;
    public GameObject parent;

    BSP_Run bs;
    [Header("BSP")]
    public Color planeColor;
    public int mapLength;
    public int GRID_SIZE;

    [Header("Pathfinding Logic")]
    AStar astar;
    public AStar_Grid grid;
    public List<Transform> targets;

    [Header("CSS")]
    public Canvas healthBar;

    [Header("Camera")]
    public Transform eg_camera;

    List<GameObject> allEnemy = new List<GameObject>();


    void Start()
    { 
        GameObject primObject = new GameObject();
        pr = primObject.AddComponent<Prim>();

        GameObject bspObject = new GameObject();
        bs = bspObject.AddComponent<BSP_Run>();

        pr.run_prim(r,c,primObject);
        bs.run_bsp(bspObject,planeColor,mapLength,GRID_SIZE);

        //astar = gameObject.AddComponent<AStar>();
        //astar.assignTarget(targets);

        Reshape(primObject.transform, bspObject.transform);
        GameObject[] allNeedles = GameObject.FindGameObjectsWithTag("NeedleBSP");
        GameObject[] allCubes = GameObject.FindGameObjectsWithTag("MazeCube");

        List<Vector3> collisionPositions = new List<Vector3>();

        foreach (GameObject cube in allCubes)
        {
            foreach (GameObject needle in allNeedles)
            {
              
                Bounds cubeBounds = cube.GetComponent<Renderer>().bounds;
                Bounds needleBounds = needle.GetComponent<Renderer>().bounds;
                Bounds crystalBounds = crystal.GetComponent<Renderer>().bounds;
                

                if (cubeBounds.Intersects(needleBounds) && !(needleBounds.Intersects(crystalBounds)))
                {


                    Vector3 collisionPosition = cube.transform.position;
                    collisionPositions.Add(collisionPosition);
                    GameObject newItem = Instantiate(getRandomObject(), collisionPosition, Quaternion.identity);

                    Bounds itemBounds = newItem.GetComponent<Renderer>().bounds;

                    newItem.layer = LayerMask.NameToLayer("Unwalkable");

                }


            }
        }
        grid.UpdateGridOnce();
        //Destroy(bspObject);

        //Destroy(primObject);
        bspObject.SetActive(false);
        primObject.SetActive(false);

        //astar.Assign();
    }

    GameObject getRandomObject()
    {
        int rnd = Random.Range(0,objects.Count);
        return objects[rnd];
    }



    void Reshape(Transform primObject, Transform bspObject)
    {
        bspObject.localScale = new Vector3(1f, 1f, 1f);
        primObject.localScale = new Vector3(5f, 5f, 5f);
        Vector3 currentPosition = bspObject.transform.position;

        bspObject.transform.Translate(-3 / 2 * mapLength * 10, 0f, -3 / 2 * mapLength * 10);

        bspObject.transform.Translate(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z, Space.World);
        primObject.transform.Translate(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z, Space.World);
        bspObject.transform.SetParent(parent.transform);
        primObject.transform.SetParent(parent.transform);
    }
}
