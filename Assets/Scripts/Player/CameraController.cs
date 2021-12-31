using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    private Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - Target.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + Target.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }
}
