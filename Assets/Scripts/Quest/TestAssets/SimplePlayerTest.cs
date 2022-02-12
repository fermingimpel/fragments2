using UnityEngine;
using UnityEngine.Serialization;


public class SimplePlayerTest : MonoBehaviour
{
    [FormerlySerializedAs("camera")]
    [Header("Camera")]
    [SerializeField] GameObject playerCamera = null;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float followSpeed = 0;

    [Header("Movement")]
    [SerializeField] float speed = 0;

    void Update()
    {
        Vector3 position = transform.position;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        position += movement * Time.deltaTime * speed;
        transform.position = position;
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, position - offset, Time.deltaTime * followSpeed);;
        playerCamera.transform.LookAt(position);
    }

    
}
