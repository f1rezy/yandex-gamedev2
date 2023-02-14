using UnityEngine;

public class Uncoupling : MonoBehaviour
{
    private HingeJoint2D _joint;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Destroy(_joint);
    }
}
