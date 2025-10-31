using UnityEngine;
using TMPro;

public class RollABallPlayerController : MonoBehaviour
{
    private Rigidbody rb;
    float movementX;
    float movementY;
    private int count = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public float speed = 10;

    public int nPickupsToWin = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();

        if (winTextObject != null)
        {
            winTextObject.SetActive(false);
        }
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        if (countText == null || winTextObject == null) return;
        countText.text = "Count: " + count.ToString();

        if (count >= nPickupsToWin)
        {
            winTextObject.SetActive(true);
        }
    }
}