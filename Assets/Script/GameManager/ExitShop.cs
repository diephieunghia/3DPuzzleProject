using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour
{
    public GameObject FButton;
    public GameObject EButton;
    public GameObject RButton;
    bool playerInRange = false;
    bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange&&GameManager.ins.playerExitStore&&once)
        {
            GameManager.ins.MoveToArea?.Invoke();
            ExitDisableButton();
            playerInRange = false;
            once = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        FButton.SetActive(true);
        playerInRange = true;
        once = true;
    }
    private void OnTriggerExit(Collider other)
    {
        FButton.SetActive(false);
        playerInRange = false;
    }
    void ExitDisableButton()
    {
        EButton.SetActive(false);
        RButton.SetActive(false);
    }
}
