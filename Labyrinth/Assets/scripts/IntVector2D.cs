using UnityEngine;
using System.Collections;

public class IntVector2D
{
    public int x;
    public int y;

    public IntVector2D(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public IntVector2D(IntVector2D vec)
    {
        x = vec.x;
        y = vec.y;
    }

    // статические конструкторы
    public static IntVector2D Up
    {
        get { return new IntVector2D(0, 1); }
    }

    public static IntVector2D Down
    {
        get { return new IntVector2D(0, -1); }
    }

    public static IntVector2D Left
    {
        get { return new IntVector2D(-1, 0); }
    }

    public static IntVector2D Right
    {
        get { return new IntVector2D(1, 0); }
    }

    public static IntVector2D One
    {
        get { return new IntVector2D(1, 1); }
    }

    public static IntVector2D Zero
    {
        get { return new IntVector2D(0, 0); }
    }


    //
    // Operators
    //
    public static IntVector2D operator +(IntVector2D a, IntVector2D b)
    {
        IntVector2D res = new IntVector2D(a);
        res.x += b.x;
        res.y += b.y;
        return res;
    }

    /*
        public static Vector2 operator /(Vector2 a, float d)
        {
            IntVector2D res = IntVector2D(a);
            res.x += a.x / d;
            res.y += a.y / d;
            return res;
        }
        */

    public static IntVector2D operator *(IntVector2D a, int d)
    {
        IntVector2D res = new IntVector2D(a);
        res.x = a.x * d;
        res.y = a.y * d;
        return res;
    }

    public static IntVector2D operator *(int d, IntVector2D a)
    {
        IntVector2D res = new IntVector2D(a);
        res.x = a.x * d;
        res.y = a.y * d;
        return res;
    }

    public static IntVector2D operator -(IntVector2D a, IntVector2D b)
    {
        IntVector2D res = new IntVector2D(a);
        res.x -= b.x;
        res.y -= b.y;
        return res;
    }

    public static IntVector2D operator -(IntVector2D a)
    {
        IntVector2D res = new IntVector2D(-a.x, -a.y);
        return res;
    }

    public static bool operator ==(IntVector2D a, IntVector2D b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(IntVector2D  a, IntVector2D b)
    {
        return a.x != b.x && a.y != b.y;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }
}


