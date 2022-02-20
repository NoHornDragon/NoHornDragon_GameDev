using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public string name;
    protected PlayerFSM playerFSM;

    public StateBase(string inputName, PlayerFSM inputPlayerFSM)
    {
        this.name = inputName;
        this.playerFSM = inputPlayerFSM;
    }

    public virtual void EnterStateAction(){}

    public virtual void UpdateAction(){}

    public virtual void ExitStateAction(){}
}
