using UnityEngine;

public class CameraRT : MonoBehaviour
{
    public Shader rtShader;

    private Camera _camera;
    void OnAwake()
    {
        _camera = GetComponent<Camera>();
    }

    [ContextMenu("same_with_main_camera")]
    void set_position()
    {
        var m = Camera.main.transform;
        var transform1 = transform;
        transform1.position = m.position;
        transform1.localEulerAngles = m.localEulerAngles;
    }


    private void LateUpdate()
    {
        if (_camera != null)
        {
            _camera.RenderWithShader(rtShader,"MyTag");
        }
        else
        {
            _camera = GetComponent<Camera>();
        }
    }
}
