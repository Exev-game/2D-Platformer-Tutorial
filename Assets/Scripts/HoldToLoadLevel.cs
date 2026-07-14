using System;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.UI;

public class HoldToLoadLevel : MonoBehaviour
{
    //Started in ep #11

    public float holdDuration = 1f; //How long you have to hold down to load the level
    public Image fillCircle;

    private float holdTimer = 0;
    private bool isHolding = false;


    public static event Action OnHoldComplete;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update toimii");
        if (isHolding)
        {
            Debug.Log("is holding toimii");
            holdTimer += Time.deltaTime;
            fillCircle.fillAmount = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                OnHoldComplete.Invoke();
                ResetHold();

            }
        }
    }

    public void onHold(InputAction.CallbackContext context)
    { 
        if (context.started)
        {
            isHolding = true;
        }
        else if(context.canceled)
        {
            //Reset holding
            ResetHold();

        }
    }

    public void ResetHold()
    {
        isHolding = false;
        holdTimer = 0;
        fillCircle.fillAmount = 0;
    }
}
