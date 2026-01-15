using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Info", menuName = "Objectinfo")]
public class SOObjectInfo : ScriptableObject
{
    public string objectName;
    [TextArea]
    public string description;
}
