using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 velocity;

    private const float YMin = -50.0f;
    private const float YMax = 70.0f;
    public Transform player;
    public Transform self;
    public LayerMask layerMask;

    public float distance = 10.0f;
    public float maxZoom = 15;
    public float minZoom = 2;
    public float zoomSpeed = 100f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private Vector3 direction;
    public float sensivity = 100f;

    void LateUpdate()
    {
        distance = Mathf.Clamp(distance + Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, minZoom, maxZoom);

        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 lookDirection = player.position - self.position;
        float d = distance;

        if (Physics.SphereCast(player.position, .45f, -lookDirection, out RaycastHit hitInfo, distance, layerMask))
        {
            d = hitInfo.distance * .8f;
        }
        direction = new Vector3(0, 0, -d);
        transform.position = Vector3.SmoothDamp(self.position, player.position + rotation * direction, ref velocity, .075f);

        transform.LookAt(player.position);
    }
}