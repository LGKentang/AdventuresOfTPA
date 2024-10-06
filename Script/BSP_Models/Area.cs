using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class Area
{

    int x, y, width, length;
    Point center;

    public Area(int x, int y, int width, int length)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.length = length;
        this.center = new Point(this.x + (this.width / 2), this.y + (this.length / 2));
    }

    public void FillArea(Color cubeColor, GameObject parent)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = parent.transform;
        cube.transform.localPosition = new Vector3(x * 32f, 1f, y * 32f);
        cube.transform.localScale = new Vector3(width*6, 19f, length*6);

        cube.tag = "NeedleBSP";

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = cubeColor;

        BoxCollider bspCollider = cube.AddComponent<BoxCollider>();
        bspCollider.size = cube.GetComponent<Renderer>().bounds.size;

        cube.SetActive(true);
        //UnityEngine.Debug.Log(cube.transform.parent.gameObject.name);

        //UnityEngine.Debug.Log(String.Format("x = {0}, y = {1}, width = {2}, length = {3}", x, y, width, length));
    }


    public int getX()
    {
        return x;
    }
    public void setX(int x)
    {
        this.x = x;
    }
    public int getY()
    {
        return y;
    }
    public void setY(int y)
    {
        this.y = y;
    }
    public int getLength()
    {
        return this.length;
    }
    public void setLength(int length)
    {
        this.length = length;
    }
    public Point getCenter()
    {
        return center;
    }
    public void setCenter(Point center)
    {
        this.center = center;
    }
    public int getWidth()
    {
        return width;
    }
    public void setWidth(int width)
    {
        this.width = width;
    }


}
