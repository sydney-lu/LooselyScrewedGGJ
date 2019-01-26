using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState<Data> : ActorState<Data> where Data : CharacterStateData
{
    public CharacterState(Data characterData) : base(characterData) { }
    
    protected override void UpdateState()
    {
        UpdateMovement();
        UpdateAnimator();
    }

    /// <summary>
    /// Character MovementUpdate, Called from StateUpdate;
    /// </summary>
    protected abstract void UpdateMovement();

    /// <summary>
    /// Character AnimatorUpdate, Called from StateUpdate;
    /// </summary>
    protected abstract void UpdateAnimator();
}
