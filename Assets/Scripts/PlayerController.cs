using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject camerHolder;
    [SerializeField] float walkSpeed, mouseSensitivity, sprintSpeed, jumpFroce, smoothTime;
    float verticalLoacalRotation;
    bool isGrounded;
    Vector3 smoothMoverVelocity, moveAmount;
    Rigidbody rd;

    private void Awake()
    {
        rd = GetComponent<Rigidbody>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoverVelocity, smoothTime);
        if(Input.GetKey(KeyCode.Space)&& isGrounded)
        {
            rd.AddForce(transform.up * jumpFroce);
        }
    }


    void Look()
    {

        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLoacalRotation = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLoacalRotation = Mathf.Clamp(verticalLoacalRotation, -90f, 90f);
        camerHolder.transform.localEulerAngles = Vector3.left * verticalLoacalRotation;
    }
    void SetGroundedState(bool _isgrounded)
    {
        isGrounded = _isgrounded;
    }
}
