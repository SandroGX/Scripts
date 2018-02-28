using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Vector2 RotMinMax = new Vector2(-10, 55);
    public float distanceToTarget = 6, cameraVelocity = 2.5f;
    public float rotationSmoothing = 0.1f;
    public float colliderOffset = 0.7f;

    Transform camTarget;
    float rotX, rotY;
    float cX, cY;


    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void LateUpdate()
    {
        if(camTarget) LookFollowTarget(camTarget.position);
    }


    private void OnApplicationFocus(bool focus)
    {
        if(focus) Cursor.lockState = CursorLockMode.Locked;
    }


    private void LookFollowTarget(Vector3 target)
    {
        rotY += Input.GetAxis("Mouse X") * cameraVelocity;
        rotX += Input.GetAxis("Mouse Y") * cameraVelocity;
        rotX = Mathf.Clamp(rotX, RotMinMax.x, RotMinMax.y);

        RotPos(target);
    }


    private void RotPos(Vector3 target)
    {
        Vector3 targetRot = new Vector3(Mathf.SmoothDampAngle(transform.eulerAngles.x, rotX, ref cX, rotationSmoothing), Mathf.SmoothDampAngle(transform.eulerAngles.y, rotY, ref cY, rotationSmoothing));
        transform.eulerAngles = targetRot;

        RaycastHit hit;
        float crrtDist;

        if (Physics.Raycast(target, -transform.forward, out hit, distanceToTarget))
            crrtDist = Vector3.Distance(target, hit.point) - colliderOffset;
        else crrtDist = distanceToTarget;

        transform.position = -transform.forward * crrtDist + target;
    }

    public void SetCamTarget(Transform target)
    {
        camTarget = target;
        rotX = camTarget.eulerAngles.x;
        rotY = camTarget.eulerAngles.y;
    }
}
