using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Time Scriptable Object", menuName = "Scriptable Objects/Time")]
public class SO_Time : ScriptableObject
{
    public List<float> timeStampList;
}
