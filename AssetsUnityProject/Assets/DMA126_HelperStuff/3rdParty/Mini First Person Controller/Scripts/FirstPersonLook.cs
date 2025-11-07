using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;


    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 velocity = Vector2.zero;
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);


        var finVert = transform.localEulerAngles + new Vector3(-frameVelocity.y, 0, 0);
        finVert.x = Mathf.DeltaAngle(0, finVert.x);
        finVert.x = Mathf.Clamp(finVert.x, -90, 90);
        transform.localEulerAngles = finVert;

        var finHoriz = character.localEulerAngles + new Vector3(0, frameVelocity.x,  0);
        character.localEulerAngles = finHoriz;
    }
}
