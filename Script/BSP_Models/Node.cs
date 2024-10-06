using UnityEngine;

public class Node
{
    private Area area;
    private Node left, right, parent;
    private int height, index;

    public Node(Area area, int height, Node parent, int index)
    {
        left = right = null;
        this.area = area;
        this.height = height;
        this.index = index;
        this.parent = parent;
    }

     public void Fill(Color planeColor, GameObject parent_obj)
    {
        if (IsLeaf())
        {
            this.area.FillArea(planeColor, parent_obj);
            return;
        }
        if (left != null)
        {
            left.Fill( planeColor, parent_obj);
        }
        if (right != null)
        {
            right.Fill(planeColor, parent_obj);
        }
    }

    public bool IsLeaf()
    {
        return left == null && right == null;
    }

    public bool IsRoot()
    {
        return parent == null;
    }

    public int getIndex()
    {
        return index;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public int getParentIndex()
    {
        return this.parent.getIndex();
    }

    public Area getArea()
    {
        return this.area;
    }

    public void setArea(Area area)
    {
        this.area = area;
    }

    public int getHeight()
    {
        return height;
    }

    public void setHeight(int height)
    {
        this.height = height;
    }

    public Node getLeft()
    {
        return this.left;
    }

    public void setLeft(Node left)
    {
        this.left = left;
    }

    public Node getRight()
    {
        return this.right;
    }

    public void setRight(Node right)
    {
        this.right = right;
    }
}
