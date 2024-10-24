using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Item/New Item")]
public class ItemData : ScriptableObject
{
    public int index;
    public string objectName;
    [TextAreaAttribute]
    public string description;
}
