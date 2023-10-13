using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class stageScript : MonoBehaviour
{
    public float tiltCoefficient;
    public Rigidbody2D stageRigidbody;
    public Camera cam1;

    void Start()
    {

    }

    void Update()
    {
        GameObject player = GameObject.Find("player");
        Vector2 playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

        stageRigidbody.centerOfMass = playerPos;
        Debug.Log(stageRigidbody.centerOfMass);

        // tilt();
        rotateCheck(playerPos);
    }

    public float getMousePos() // gets horizontal mouse position relative to the screen
    {
        Vector3 mousePos = Input.mousePosition;
        float halfway = Screen.width / 2;
        float screenDec = 2 * (mousePos.x - halfway) / Screen.width; // x position on screen in the range [-1, 1]
        return screenDec;
    }

    public void tilt() // tilts the stage
    {
        float stageAngle = tiltCoefficient * getMousePos();

        // Debug.Log(stageAngle); // remove later

        stageRigidbody.MoveRotation(-stageAngle);
    }

    public void rotateCheck(Vector2 playerPosPass)
    {
        Vector3 playerPos = cam1.WorldToScreenPoint(playerPosPass); // gets player position on screen
        Vector3 mousePos = Input.mousePosition; // gets mouse position on screen

        if (mousePos.x >= playerPos.x) // if the mouse is to the right of or in line with the player
        {
            float mouseXP = mousePos.x - playerPos.x; // relative x position of the mouse to the player
            float mouseYP = mousePos.y - playerPos.y; // relative y position of the mouse to the player
            double angleP = Math.Atan(mouseYP / mouseXP) * (180 / Math.PI); // calculates the angle from the player to the mouse, then converts to degrees

            stageRigidbody.rotation = (float)(angleP); // sets player rotation to that angle
        }
        else // if the mouse is to the left
        {
            float mouseXP = mousePos.x - playerPos.x;
            float mouseYP = mousePos.y - playerPos.y;
            double angleP = (Math.Atan(mouseYP / mouseXP) + Math.PI) * (180 / Math.PI); // only difference is adding pi to correct for the limited range of arctan

            stageRigidbody.rotation = (float)(angleP);
        }
    }
}
