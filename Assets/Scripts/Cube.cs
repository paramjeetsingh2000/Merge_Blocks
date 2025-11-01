using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _text;
    public int cubeNum;

    public void SetCube(int cubeNum, Color32 col)
    {
        this.cubeNum = cubeNum;
        GetComponent<MeshRenderer>().material.color = col;

        foreach (var c in _text)
        {
            c.text = cubeNum.ToString();
        }
    }
}