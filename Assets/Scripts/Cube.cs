using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _text;
    public int cubeNum;
    public bool hasMerged;
    public bool canMerge = true;


    public void SetCube(int cubeNum, Color32 col)
    {
        this.cubeNum = cubeNum;
        GetComponent<MeshRenderer>().material.color = col;

        foreach (var c in _text)
        {
            c.text = cubeNum.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasMerged || !CubeManager.Instance.canMerge) return;


        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null)
        {
            if (otherCube.cubeNum == this.cubeNum && ! otherCube.hasMerged)
            {
                this.hasMerged = true;
                otherCube.hasMerged = true;
                int newNum = this.cubeNum * 2;
                Vector3 mergePosition = (this.transform.position + otherCube.transform.position) / 2f;
                CubeManager.Instance.SpawnMergedCube(newNum, mergePosition);
                GameManager.Instance.AddScore(newNum);
                GameManager.Instance.WinGame(newNum);
                CubeManager.Instance.UpdateMaxNum(newNum);
   
                Destroy(this.gameObject);
                Destroy(otherCube.gameObject);
            }
        }
    }
}