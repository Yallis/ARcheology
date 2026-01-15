using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    public bool IsOccupied()
    {
        return transform.childCount > 0;
    }
}
