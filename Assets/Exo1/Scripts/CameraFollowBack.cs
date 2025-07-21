using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBack : MonoBehaviour
{

    CameraOrbit cameraOrbit;
    PlayerController playerManager;
    public Transform cameraPivot;
    private Transform cameraTransform;

    public Transform target;
    public float smoothSpeed = 0.125f;
    //    public float Distance = 3;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    [Header("Camera Look Adv Rotation")]
    public float MouseSensitivityAdv = 1f;
    protected Vector3 _LocalRotation;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    [Header("Camera Look Type Collision")]
    public LayerMask collisionLayers; // camera will collide with this
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionOffset = 0.2f; // how much the camera will jump of its colliding
    public float cameraCollisionRadius = 0.2f;



    private void Awake()
    {
        cameraOrbit = FindObjectOfType<CameraOrbit>();
        playerManager = FindObjectOfType<PlayerController>();
        target = FindObjectOfType<PlayerController>().transform;
        cameraTransform = Camera.main.transform;
        cameraPivot = cameraTransform.parent;
        defaultPosition = cameraTransform.localPosition.z;

    }

    public void FollowTarget()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.parent.position, desiredPosition, smoothSpeed);
        transform.parent.position = smoothedPosition;

        if (playerManager.cameraType != PlayerController.CameraTypeEnum.Advanced)
        {
            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.parent.rotation, desiredrotation, smoothSpeed);
            transform.parent.rotation = smoothedrotation;
        }
        else if (playerManager.cameraType == PlayerController.CameraTypeEnum.Advanced)
        {
            if (!playerManager.LockedCamera)
            {
                _LocalRotation.x = cameraOrbit.x * MouseSensitivityAdv;

                if (_LocalRotation.x < minimumPivotAngle)
                    _LocalRotation.x = minimumPivotAngle;
                else if (_LocalRotation.x > maximumPivotAngle)
                    _LocalRotation.x = maximumPivotAngle;

                rotationOffset.x = rotationOffset.x + _LocalRotation.x;

                if (rotationOffset.x < minimumPivotAngle)
                    rotationOffset.x = minimumPivotAngle;
                else if (rotationOffset.x > maximumPivotAngle)
                    rotationOffset.x = maximumPivotAngle;

                Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
                Quaternion smoothedrotation = Quaternion.Lerp(transform.parent.rotation, desiredrotation, smoothSpeed);
                transform.parent.rotation = smoothedrotation;
            }
        }
    }

    public void HandleCameraBackCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
