using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Colisión")]
    [Tooltip("Las capas con las que la cámara colisionará.")]

    [SerializeField] private LayerMask m_collisionMask;
    [Tooltip("Distancia a la que la cámara se mantendrá del objeto con el que colisiona.")]

    [SerializeField] private float m_collisionBuffer = 0.2f;
    
    [SerializeField] private float m_cameraPositionLerpSpeed = 5f;

    protected float m_yaw = 0f;
    
    protected float m_pitch = 0f;

    private Transform _cameraTransform;
    
    private float _targetDistance;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cameraTransform = transform.GetChild(0);
        _targetDistance = _cameraTransform.localPosition.z;
    }

    private void LateUpdate()
    {

        HandleCollisions();
    }



    private void HandleCollisions()
    {
        Vector3 desiredPosition = transform.TransformPoint(new Vector3(0, 0, -_targetDistance));
        RaycastHit hit; // Corregido: RaycastHit en lugar de ReaycastHit

        if (Physics.Linecast(transform.position, desiredPosition, out hit, m_collisionMask))
        {
            desiredPosition = hit.point + hit.normal * m_collisionBuffer;
        }

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, desiredPosition, Time.deltaTime * m_cameraPositionLerpSpeed);
    }

}