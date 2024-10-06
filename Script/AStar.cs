using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour
{

    public List<Transform> seekers;
    public List<Transform> targets;

    public AStar_Grid grid;
    public float speed = 5f; // seeker speed

    private List<AStar_Node>[] paths;
    private int[] currentPathIndices;

    public bool toldToSeek;

    Transform closest;

    void Awake()
    {
        seekers = new List<Transform>();
        toldToSeek = true;
    }

    public void addSeek(Transform item)
    {
        this.seekers.Add(item);
    }
    public void removeSeek(Transform item)
    {
        this.seekers.Remove(item);
    }

    public void assignTarget(List<Transform> t)
    {
        this.targets = t;
    }

    public void Assign()
    {
        if (grid == null) grid = GetComponent<AStar_Grid>();
        paths = new List<AStar_Node>[seekers.Count];
        currentPathIndices = new int[seekers.Count];
        //Debug.Log(seekers.Count);

        //foreach (Transform t in seekers) Debug.Log(t);
    }

    void Update()
    {
        //if (seekers.Count == 0) print("None");//Debug.Log("no assigned seekers");
        
        if (seekers.Count > 0 && toldToSeek)
        {
            for (int i = 0; i < seekers.Count; i++)
            {
                Transform closestTarget = null;
                if (seekers[i])
                {
                    closestTarget = GetClosestTarget(seekers[i].position);
                    closest = closestTarget;
                }

                if (closestTarget != null)
                {
                    FindPath(seekers[i].position, closestTarget.position, i);
                    if (paths[i] != null && paths[i].Count > 0)
                    {
                        if (currentPathIndices[i] < paths[i].Count)
                        {
                            Vector3 targetPosition = paths[i][currentPathIndices[i]].worldPosition;
                            Vector3 moveDirection = (targetPosition - seekers[i].position).normalized;
                            seekers[i].Translate(moveDirection * speed * Time.deltaTime);

                            seekers[i].LookAt(targetPosition);

                            float nodeRadius = grid.nodeRadius;
                            if (Vector3.Distance(seekers[i].position, targetPosition) <= nodeRadius)
                            {
                                currentPathIndices[i]++;

                                if (currentPathIndices[i] >= paths[i].Count)
                                {
                                    paths[i] = null;
                                    currentPathIndices[i] = 0;
                                }
                            }

                        }
                    }
                }
            }
        }

    }

    public Transform RetreiveClosestTarget()
    {
        return closest;
    }

    Transform GetClosestTarget(Vector3 position)
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            if (target)
            {
                float distance = Vector3.Distance(position, target.position);
                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }
        }

        return closestTarget;
    }


    void FindPath(Vector3 startPos, Vector3 targetPos, int seekerIndex)
    {

        AStar_Node startNode = grid.NodeFromWorldPoint(startPos);
        AStar_Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<AStar_Node> openSet = new List<AStar_Node>();
        HashSet<AStar_Node> closedSet = new HashSet<AStar_Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            AStar_Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode, seekerIndex);
                return;
            }

            foreach (AStar_Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(AStar_Node startNode, AStar_Node endNode, int seekerIndex)
    {
        paths[seekerIndex] = new List<AStar_Node>();
        AStar_Node currentNode = endNode;

        while (currentNode != startNode)
        {
            paths[seekerIndex].Add(currentNode);
            currentNode = currentNode.parent;
        }
        paths[seekerIndex].Reverse();

        grid.path = paths[seekerIndex];
    }

    int GetDistance(AStar_Node nodeA, AStar_Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
