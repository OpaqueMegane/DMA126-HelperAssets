/*
 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTeleportPoint : MonoBehaviour
{
    static GUIDrawer _guiDrawer;
    static List<CheatTeleportPoint> _allPoints = new();

    public Transform thingToTeleportHere;

    void Awake()
    {
        _allPoints.Add(this);
        if (_guiDrawer == null)
        {
            _guiDrawer = new GameObject().AddComponent<GUIDrawer>();
            DontDestroyOnLoad(_guiDrawer.gameObject);
        }
    }

    private void OnDestroy()
    {
        _allPoints.Remove(this);
    }

    private void Execute()
    {
        TeleportThing(thingToTeleportHere, this.transform.position);



    }

    void TeleportThing(Transform thing, Vector3 position)
    {
        var rb = thingToTeleportHere.GetComponent<Rigidbody>();
        var cc = thingToTeleportHere.GetComponent<CharacterController>();
        if (cc != null)
        {
            if (!cc.enabled)
            {
                cc = null; //don't reenable if already disabled
            }
            else
            {
                cc.enabled = false;
            }
        }

        this.thingToTeleportHere.position = this.transform.position;
        this.thingToTeleportHere.rotation = this.transform.rotation;

        if (rb != null && !rb.isKinematic)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (cc != null)
        {
            cc.enabled = true;
        }
    }

    public class GUIDrawer : MonoBehaviour
    {
        bool showGUI = false;
        GUIStyle textWrapping;

        bool cursorVisible;
        CursorLockMode cursorLockMode;

        private void Awake()
        {

        }
        private void Update()
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.C))
            {
                showGUI = !showGUI;
                if (showGUI)
                {
                    this.cursorVisible = Cursor.visible;
                    this.cursorLockMode = Cursor.lockState;
                    
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = this.cursorVisible;
                    Cursor.lockState = this.cursorLockMode;
                }
            }
        }



        private void OnGUI()
        {
            if (!showGUI) return;

            if (textWrapping == null)
            {
                textWrapping = new GUIStyle(GUI.skin.button);
                textWrapping.wordWrap = true;
            }

            float btnW = 200;
            float btnH = 50;
            float margin = 5;

            int nBtnsHoriz = Mathf.Max(1, Mathf.FloorToInt((Screen.width - margin) / (btnW + margin)));

            int i = -1;
            foreach (var point in _allPoints)
            {
                i++;
                int bx = i % nBtnsHoriz;
                int by = i / nBtnsHoriz;

                if (GUI.Button(new Rect(margin + bx * (margin + btnW), margin + by * (margin + btnH), btnW, btnH), point.name, textWrapping))
                {
                    point.Execute();
                }
            }
        }
    }
}
