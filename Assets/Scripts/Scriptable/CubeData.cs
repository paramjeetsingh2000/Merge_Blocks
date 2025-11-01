using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObjects/CubeData", order = 1)]
public class CubeData : ScriptableObject
{
   public List<OneCubeData> data;
}

[Serializable]
public class OneCubeData
{
    public int cubeNum;
    public Color32 cubeColor;
}
