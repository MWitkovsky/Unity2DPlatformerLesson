using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {

    public KeyCode jumpKey;

    private PlayerController playerController;
    private float horizontal;
    private bool jumpKeyPressed;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        jumpKeyPressed = Input.GetKeyDown(jumpKey);
        playerController.ProcessInput(horizontal, jumpKeyPressed);
    }
}
