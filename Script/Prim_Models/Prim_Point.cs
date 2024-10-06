using System;

public class Prim_Point : Point
{
    Prim_Point parent;

    public Prim_Point(int x, int y, Prim_Point p)
        : base(x, y)
    {
        parent = p;
    }

    public Prim_Point opposite()
    {
        if (this.getX() != parent.getX())
            return new Prim_Point(this.getX() + Math.Sign(this.getX() - parent.getX()), this.getY(), this);
        if (this.getY() != parent.getY())
            return new Prim_Point(this.getX(), this.getY() + Math.Sign(this.getY() - parent.getY()), this);
        return null;
    }
}
