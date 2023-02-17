using System;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private HingeJoint2D _joint;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Unhook();
        Swing();
    }

    private void Swing()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        horizontalInput *= _moveSpeed * Time.deltaTime;
        _rigidbody2D.AddForce(new Vector2(horizontalInput, 0));
    }

    private void Unhook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _joint.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out EndOfRope endOfRope))
        {
            _joint.enabled = true;
            Rigidbody2D colliderRb = other.gameObject.GetComponent<Rigidbody2D>();
            _joint.connectedBody = colliderRb;
        }
    }
}
