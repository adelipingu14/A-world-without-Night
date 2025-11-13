using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMushRoom : MonoBehaviour
{
    public int jumpPower;
    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MushJump()
    { 
        _rigidbody.AddForce(transform.up * jumpPower);
        _rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerBody = other.GetComponent<Rigidbody>();
            if (playerBody != null)
            {
                playerBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
