using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorState<Data> : BaseState where Data : ActorStateData
{
    protected Data data;

    public ActorState(Data characterData) : base(characterData.StateM)
    {
        data = characterData;
    }

    public override void FixedUpdate()
    {
        if (!stateManager.IsPaused)
            UpdatePhysics();
    }

    /// <summary>
    /// Physics Update, Called From ActorState FixedUpdate
    /// </summary>
    protected abstract void UpdatePhysics();
    protected override void UpdateState() { }
}
