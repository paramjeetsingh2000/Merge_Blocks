using UnityEngine;

public class CubeManager : MonoBehaviour 
{
    public static CubeManager Instance { get; private set; }
    [SerializeField] private Transform cubePos;
    [SerializeField] private Cube cube, currentCube;
    [SerializeField] private float slideSpeed = 0.01f, force;

    private Vector2 lastPointerPosition;
    private bool isDragging = false;

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
        isDragging = false;
        currentCube.GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Impulse);
        Invoke("AddCube", 0.3f);
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
        currentCube = Instantiate(cube,cubePos.position, Quaternion.identity);
    }
}
