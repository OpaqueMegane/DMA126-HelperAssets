using UnityEngine;

public class CameraControllerSimple : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0,15,0);
    public bool followX = true;
    public bool followY = true;
    void Start()
    {
        if (offset == Vector3.zero)
        {
            recordOffset();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        
        //don't change x position, if not following in x
        if (!followX)
        {
            targetPosition.x = this.transform.position.x; //keep target position the same =
        }

        //don't change y position, if not following in y
        if (!followY)
        {
            targetPosition.y = this.transform.position.y; //keep target position the same =
        }

        this.transform.position = targetPosition;

    }

    [ContextMenu("Record Offset")]
    void recordOffset()
    {
        offset = transform.position - player.transform.position;
    }
}
