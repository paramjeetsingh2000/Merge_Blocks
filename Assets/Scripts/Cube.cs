using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float slideSpeed = 0.01f, force; 

    private Vector2 lastPointerPosition;
    private bool isDragging = false;

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
        GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.touchCount > 0)
            {
                transform.position += new Vector3((lastPointerPosition.x - Input.touches[0].position.x) * -slideSpeed * Time.deltaTime,0f,0f);
                lastPointerPosition = Input.touches[0].position;
                if (transform.position.x > 1.12)
                {
                    transform.position = new Vector3 (1.12f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x < -1.12)
                {
                    transform.position = new Vector3(-1.12f, transform.position.y, transform.position.z);
                }
            }
            else
            {
                transform.position += new Vector3((lastPointerPosition.x - Input.mousePosition.x) * -slideSpeed * Time.deltaTime, 0f, 0f);
                lastPointerPosition = Input.mousePosition;
                if (transform.position.x > 1.12)
                {
                    transform.position = new Vector3(1.12f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x < -1.12)
                {
                    transform.position = new Vector3(-1.12f, transform.position.y, transform.position.z);
                }
            }
        }
    }
}