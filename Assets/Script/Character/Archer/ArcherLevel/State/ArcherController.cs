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

    SkillHandler skillHandler;
    private void Awake()
    {
        skillHandler = GetComponent<SkillHandler>();
        characterController = GetComponent<CharacterController>();
        bb.groundCheck = groundCheck;
        bb.groundMask = groundMask;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveStateHandler=new MovementStateHandler(bb,this.transform,characterController);
        input = new PlayerInput(bb,transform,skillHandler);
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
