using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Calculator
{
    public static Vector3 MulElements(Vector3 a, Vector3 b)
        => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
}
