using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArcherController : MonoBehaviour
{
    CharacterController characterController;

    public ArcherBlackBoard bb=new ArcherBlackBoard();

    //Input from keyboard script
    PlayerInput input;
    //Handle Movement state
    MovementStateHandler moveStateHandler;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        bb.groundCheck = groundCheck;
        bb.groundMask = groundMask;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveStateHandler=new MovementStateHandler(bb,this.transform,characterController);
        input = new PlayerInput(bb);
    }

    // Update is called once per frame
    void Update()
    {       
        moveStateHandler.HandleMovementState();
    }
    private void LateUpdate()
    {
        input.HandleInput();
    }
}
