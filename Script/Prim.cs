using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Prim : MonoBehaviour
{
	System.Random random = new System.Random();

    public void run_prim(int r, int c, GameObject parent)
	{

		char[][] map = new char[r][];
		for (int i = 0; i < r; i++)
		{
			map[i] = new char[c];
		}

		construct(r, c, map);
		prim(r, c, map);
		build(r, c, map, parent);
	}

	public void construct(int r, int c, char[][] map)
	{
		for (int i = 0; i < r; i++)
		{
			for (int j = 0; j < c; j++)
			{
				map[i][j] = '*';
			}
		}
	}

	public void build(int r, int c, char[][] map, GameObject parent)
	{
		Vector3 startPosition = new Vector3(-c / 2f, 0f, r / 2f);

		for (int i = 0; i < r; i++)
		{
			for (int j = 0; j < c; j++)
			{
				Vector3 position = startPosition + new Vector3(j, 0f, -i);

				if (map[i][j] == '*')
				{
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = position;
					cube.transform.SetParent(parent.transform);
					
					cube.tag = "MazeCube";

					BoxCollider bspCollider = cube.AddComponent<BoxCollider>();
					bspCollider.size = cube.GetComponent<Renderer>().bounds.size;
					cube.SetActive(true);
				}
			}
		}
	}

	public void addPoints(List<Prim_Point> unexplored, Prim_Point p, char[][] maze)
	{
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0 || x != 0 && y != 0)
					continue;
				try
				{
					if (maze[p.getX() + x][p.getY() + y] == ' ')
						continue;
				}
				catch (Exception)
				{
					continue;
				}
				unexplored.Add(new Prim_Point(p.getX() + x, p.getY() + y, p));
			}
		}
	}

	public void prim(int numRows, int numColumns, char[][] maze)
	{

		Prim_Point start = new Prim_Point((int)(random.NextDouble() * numRows), (int)(random.NextDouble() * numColumns), null);
		maze[start.getX()][start.getY()] = 'O';

		List<Prim_Point> unexplored = new List<Prim_Point>();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				addPoints(unexplored, start, maze);
			}
		}

		Prim_Point last = null;
		while (unexplored.Count > 0)
		{
			int randomIndex = (int)(random.NextDouble() * unexplored.Count);
			Prim_Point curr = unexplored[randomIndex];
			unexplored.RemoveAt(randomIndex);
			Prim_Point opposite = curr.opposite();
			try
			{
				if (maze[curr.getX()][curr.getY()] == '*')
				{
					if (maze[opposite.getX()][opposite.getY()] == '*')
					{
						maze[curr.getX()][curr.getY()] = ' ';
						maze[opposite.getX()][opposite.getY()] = ' ';
						last = opposite;
						for (int x = -1; x <= 1; x++)
						{
							for (int y = -1; y <= 1; y++)
							{
								addPoints(unexplored, opposite, maze);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}

			if (unexplored.Count == 0)
				maze[last.getX()][last.getY()] = '!';
		}
	}
}
