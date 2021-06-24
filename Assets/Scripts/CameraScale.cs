using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public float orthographicSize = 5;
    public float aspect = 1.33333f;
    /**public GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;**/
    void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(
        -orthographicSize * aspect, orthographicSize * aspect,
        -orthographicSize, orthographicSize,
        Camera.main.nearClipPlane, Camera.main.farClipPlane);

    }
}
