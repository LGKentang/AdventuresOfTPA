using UnityEngine;
using System.Collections;

public class AStar_Node
{

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public AStar_Node parent;

	public AStar_Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
	{
		this.walkable = walkable;
		worldPosition = worldPos;
		this.gridX = gridX;
		this.gridY = gridY;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}