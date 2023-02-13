using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform pivot;
    public Vector3 pivotOffset = Vector3.zero;
    public float distance = 200.0f;
    public float minDistance = 2f;
    public float maxDistance = 200f;
    public float zoomSpeed = 5f;
    public float xSpeed = 250.0f;
    public float ySpeed = 250.0f;
    public bool allowYTilt = true;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float targetX = 0f;
    private float targetY = 0f;
    public float targetDistance = 0f;
    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;
    public static float targetXMove = 0f;
    public static float targetYMove = 0f;
    public Vector3 rotationOffect = new Vector3(80, 25);
    private void Start()
    {
        var angles = transform.eulerAngles;
        targetX = x = angles.x;
        targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;
    }
    private void LateUpdate()
    {
        if (!pivot) return;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0.0f) targetDistance -= zoomSpeed;
        else if (scroll < 0.0f)
            targetDistance += zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        //if (Input.GetMouseButton(0))
        //{
        //    targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        //    if (allowYTilt)
        //    {
        //        targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        //        targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
        //    }
        //}
        //else if (Input.GetMouseButton(1))
        //{
        //    MoveObject();
        //}
        x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
        y = allowYTilt ? Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f) : targetY;
        Quaternion rotation = Quaternion.Euler(y + rotationOffect.x, x + rotationOffect.y, 0);
        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
        transform.rotation = rotation;
        transform.position = position;
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    private void MoveObject()
    {
        targetXMove += Input.GetAxis("Mouse X") * 0.02f;
        targetYMove -= Input.GetAxis("Mouse Y") * 0.02f;

        transform.GetComponent<Camera>().lensShift = new Vector2(-targetXMove, targetYMove);
    }
}
