using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
       return this.y;
    }

    public int getX()
    {
        return this.x;
    }

}
