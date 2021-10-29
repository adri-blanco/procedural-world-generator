using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
  public float Speed = 5.0f;
  public float RotationSpeed = 64.0f;
  public float Jump = 5;
  public Camera Eyes;

  private Rigidbody Physics;

  // Start is called before the first frame update
  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    Physics = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    transform.Translate(new Vector3(horizontal * Time.deltaTime * Speed, 0.0f, vertical * Time.deltaTime * Speed));

    float rotationX = Input.GetAxis("Mouse X");
    float rotationY = Input.GetAxis("Mouse Y");
    float nextEyesXRotation = Eyes.transform.rotation.x - rotationY;
    if (nextEyesXRotation > -0.5f && nextEyesXRotation < 0.5f)
    {
      Eyes.transform.Rotate(new Vector3(-rotationY, 0, 0) * Time.deltaTime * RotationSpeed);
    }
    transform.Rotate(new Vector3(0, rotationX, 0) * Time.deltaTime * RotationSpeed);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      Physics.AddForce(new Vector3(0, Jump, 0), ForceMode.Impulse);
    }
  }
}
