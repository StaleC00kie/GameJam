using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Cinemachine;

public class PlayerController : NetworkTransform
{
    public CharacterController controller { get; private set; }

    public float maxSpeed = 2.0f;

    public float acceleration = 10.0f;
    public float braking = 10.0f;

    private Vector3 velocity;

    private float xRotation;

    private Camera cam;

    [SerializeField]
    private Transform playerBody;

    private Transform playerCameraAnchor;

    [SerializeField]
    private float gravity;
    protected override void Awake()
    {
        base.Awake();
        CacheController();
        CacheCamera();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CacheController()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();

            Assert.Check(controller != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(CharacterController)} component.");
        }
    }

    private void CacheCamera()
    {
        cam = Camera.main;
    }

    public override void Spawned()
    {
        base.Spawned();
        CacheController();

        // Caveat: this is needed to initialize the Controller's state and avoid unwanted spikes in its perceived velocity
        controller.Move(transform.position);
    }

    protected override void CopyFromBufferToEngine()
    {
        // Trick: CC must be disabled before resetting the transform state
        controller.enabled = false;

        // Pull base (NetworkTransform) state from networked data buffer
        base.CopyFromBufferToEngine();

        // Re-enable CC
        controller.enabled = true;
    }

    public virtual void Move(Vector3 direction)
    {
        var previousPos = transform.position;
        var horizontalVel = default(Vector3);

        if(direction == default)
        {
            horizontalVel = Vector3.Lerp(horizontalVel, default, braking * Runner.DeltaTime);
        }
        else
        {
            horizontalVel = Vector3.ClampMagnitude(horizontalVel + direction * acceleration * Runner.DeltaTime, maxSpeed);
        }

        velocity.y = gravity * Runner.DeltaTime;
        velocity.x = horizontalVel.x;
        velocity.z = horizontalVel.z;
        controller.Move(velocity);

        velocity += (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
    }

    public void ThrustUp()
    {

    }

    public virtual void CameraLook(Vector2 mouseInput)
    {
        xRotation -= mouseInput.y;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    public void RotateBody(float mouseX)
    {
        playerBody.Rotate(Vector3.up * mouseX * Time.deltaTime);
    }
}
