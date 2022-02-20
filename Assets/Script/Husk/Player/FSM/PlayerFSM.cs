using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    private StateBase currentState;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(currentState != null)
            currentState.UpdateAction();
    }

    


}
