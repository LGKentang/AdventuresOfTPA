using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

public class BSP
{
    private int iteration;
    private Node[] nodes;
    private int len;

    System.Random rnd = new System.Random();
    public int randomNumber(int min, int max)
    {
        if (min == max) return max;
        return rnd.Next(max - min) + min;
    }

    public BSP(int iteration)
    {
        this.iteration = iteration;
        this.len = (int)(Math.Pow(2, iteration));
        this.nodes = new Node[len];
    }

    public Node split(Area area, int iter, Node parent, int idx)
    {
        Node newNode = new Node(area, iter, parent, idx);
        nodes[idx] = newNode;
        if (iter < iteration)
        {
            Area[] res = splitArea(area);
            if (res[0] == null || res[1] == null) return newNode;

            newNode.setLeft(split(res[0], iter + 1, newNode, (idx * 2) + 1));
            newNode.setRight(split(res[1], iter + 1, newNode, (idx * 2) + 2));
        }
        return newNode;
    }

    public Area[] splitArea(Area c)
    {
        Area c1 = null, c2 = null;
        Area[] res = { c1, c2 };
        int r = splitDirection(randomNumber(1, 3), c.getWidth(), c.getLength());

        if (r == -1)
        {
            return res;
        }
        if (r == 1)
        {
            int w1 = randomNumber(1, c.getWidth());

            c1 = new Area(c.getX(), c.getY(), w1, c.getLength());
            c2 = new Area(c.getX() + w1, c.getY(), c.getWidth() - w1, c.getLength());
        }
        else
        {
            int h1 = randomNumber(1, c.getLength());
            c1 = new Area(c.getX(), c.getY(), c.getWidth(), h1);
            c2 = new Area(c.getX(), c.getY() + h1, c.getWidth(), c.getLength() - h1);
        }
        res[0] = c1;
        res[1] = c2;

        return res;
    }


    private int splitDirection(int random, int w, int h)
    {
        if (w <= 1 && h <= 1)
        {
            return -1;
        }
        else if (w <= 1)
        {
            return 2;
        }
        else if (h <= 1)
        {
            return 1;
        }
        else
        {
            return random;
        }
    }


    public Node[] getLeafNodes()
    {
        int n = len / 2 - 1;
        Node[] res = new Node[n + 2];
        int j = 0;

        for (int i = n; i < len - 1; i++)
        {
            if (nodes[i] == null) continue;
            res[j] = nodes[i];
            j++;
        }
        return res;
    }
}
