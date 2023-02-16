using UnityEngine;

public class Uncoupling : MonoBehaviour
{
    private HingeJoint2D _joint;
    private float moveSpeed = 0.1f;
    private Rigidbody2D rb;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_joint);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnEnterCollider2D(Collider2D collision)
    {
        Rigidbody2D rb_col = collision.GetComponent<Rigidbody2D>();
        gameObject.AddComponent<HingeJoint2D>().connectedBody = rb_col;
    }
}
