using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public static CubeManager Instance { get; private set; }
    [SerializeField] private Transform cubePos;
    [SerializeField] private Cube cube, currentCube;
    [SerializeField] private float slideSpeed = 0.01f, force;
    [SerializeField] private CubeData cubeData;
    private int currentMaxNum = 2;
    private bool canThrow = true;
    public bool canMerge = true;

    private Vector2 lastPointerPosition;
    private bool isDragging = false;
    int maxNum = 2048;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        AddCube();
    }

    public void OnPointerDown()
    {
        if (!canThrow || currentCube == null) return;

        if (Input.touchCount > 0)
        {
            lastPointerPosition = Input.touches[0].position;
            isDragging = true;
        }
        else
        {
            lastPointerPosition = Input.mousePosition;
            isDragging = true;
        }
    }

    public void OnPointerUp()
    {
        if (!canThrow || currentCube == null) return;

        canThrow = false;
        isDragging = false;
        canMerge = false;


        Rigidbody rb = currentCube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(Vector3.forward * force, ForceMode.Impulse);

        Invoke(nameof(EnableThrow), 0.3f);
        Invoke(nameof(AddCube), 0.3f);
        Invoke(nameof(EnableMerging), 0.3f);

        currentCube = null;
    }

    private void EnableThrow()
    {
        canThrow = true;
    }
    private void EnableMerging()
    {
        canMerge = true;
    }




    private void Update()
    {
        if (isDragging)
        {
            if (Input.touchCount > 0)
            {
                currentCube.transform.position += new Vector3((lastPointerPosition.x - Input.touches[0].position.x) * -slideSpeed * Time.deltaTime, 0f, 0f);
                lastPointerPosition = Input.touches[0].position;
                if (currentCube.transform.position.x > 1.12)
                {
                    currentCube.transform.position = new Vector3(1.12f, currentCube.transform.position.y, currentCube.transform.position.z);
                }
                else if (currentCube.transform.position.x < -1.12)
                {
                    currentCube.transform.position = new Vector3(-1.12f, currentCube.transform.position.y, currentCube.transform.position.z);
                }
            }
            else
            {
                currentCube.transform.position += new Vector3((lastPointerPosition.x - Input.mousePosition.x) * -slideSpeed * Time.deltaTime, 0f, 0f);
                lastPointerPosition = Input.mousePosition;
                if (currentCube.transform.position.x > 1.12)
                {
                    currentCube.transform.position = new Vector3(1.12f, currentCube.transform.position.y, currentCube.transform.position.z);
                }
                else if (currentCube.transform.position.x < -1.12)
                {
                    currentCube.transform.position = new Vector3(-1.12f, currentCube.transform.position.y, currentCube.transform.position.z);
                }
            }
        }
    }
    public void AddCube()
    {
        currentCube = Instantiate(cube, cubePos.position, Quaternion.identity);
        currentCube.canMerge = false;


        List<OneCubeData> data = new();

        foreach (var v in cubeData.data)
        {
             if (v.cubeNum <= Mathf.Min(maxNum, currentMaxNum))

                {
                    data.Add(v);
            }
            else break;
        }

        OneCubeData oneData = data[Random.Range(0, data.Count)];

        currentCube.SetCube(oneData.cubeNum, oneData.cubeColor);
    }

    public void SpawnMergedCube(int number,Vector3 position)
    {
        OneCubeData newCubeData = null;
        for (int i = 0; i < cubeData.data.Count; i++)
        {
            if (cubeData.data[i].cubeNum == number)
            {
                newCubeData = cubeData.data[i];
                break;
            }
        }
        if (newCubeData == null)
        {
            Debug.LogError("no cubedata found for number : " +  number);
        }
        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.SetCube(newCubeData.cubeNum, newCubeData.cubeColor);

    }

    public void UpdateMaxNum(int mergedNum)
    {
        if (mergedNum > currentMaxNum)
            currentMaxNum = mergedNum;
    }


}
