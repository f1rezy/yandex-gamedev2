using System.Collections;
using System.Collections.Generic;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Hook(other);
    }

    private void Swing()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        horizontalInput *= _moveSpeed * Time.deltaTime;
        _rigidbody2D.AddForce(new Vector2(horizontalInput, 0));
    }

    private void Hook(Collision2D other)
    {
        if (_joint.enabled)
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out EndOfRope endOfRope))
        {
            _joint.enabled = true;
            Rigidbody2D colliderRb = other.gameObject.GetComponent<Rigidbody2D>();
            _joint.connectedBody = colliderRb;
        }
    }

    private void Unhook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = _joint.connectedBody.gameObject.GetComponent<Collider2D>();
            collider.enabled = false;
            _joint.enabled = false;
            StartCoroutine(Waiting(collider));
        }
    }

    private IEnumerator Waiting(Collider2D collider)
    {
        yield return new WaitForSeconds(1f);
        RestoreCollider(collider);
    }

    private void RestoreCollider(Collider2D collider)
    {
        collider.enabled = true;
    }
}