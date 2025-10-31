using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Follower : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public Transform targetTransform;
    Pose followTarget;

    
    Vector3 lastPosition = Vector3.zero;

    public bool pointInMoveDirection;
    public float pointInMoveDirectionSpeed = 1500;

    void Start()
    {
        lastPosition = targetTransform.position;
    }


    void FixedUpdate()
    {
        MovePosition();
    }

    private void LateUpdate()
    {
        MovePosition();

        var curPos = targetTransform.position;
        if (pointInMoveDirection)
        {
            Vector3 moveDirection = curPos - lastPosition;
            Vector3 moveHorizontal = Vector3.Scale(moveDirection, new Vector3(1, 0, 1));
            if (moveHorizontal.sqrMagnitude > 0)
            {
                float targetYAngle = Vector3.SignedAngle(Vector3.forward, moveHorizontal, Vector3.up);

                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(0, targetYAngle, 0), Time.deltaTime * pointInMoveDirectionSpeed);
            }
        }

        lastPosition = targetTransform.position;
    }

    void MovePosition()
    {
        followTarget = new(targetTransform.position + offset, targetTransform.rotation);
        this.transform.position = followTarget.position;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Follower))]
    public class CustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = target as Follower;
            if (GUILayout.Button("Set Offset"))
            {
                script.offset = script.transform.position - script.targetTransform.position;
            }
        }
    }
#endif
}
