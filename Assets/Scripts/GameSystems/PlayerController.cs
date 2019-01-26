using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private Actor actor;
    public Actor PlayerActor { get { return actor; } }

    #endregion

    #region Input
    private InputTrigger rightTriggerState = new InputTrigger("RightTrigger", TriggerType.Trigger);
    private InputTrigger leftTriggerState = new InputTrigger("LeftTrigger", TriggerType.Trigger);

    public InputTrigger RightTrigger
    {
        get { return rightTriggerState; }
    }
    public InputTrigger LeftTrigger
    {
        get { return leftTriggerState; }
    }
    #endregion

    #region Main
    protected void Awake()
    {
        actor = GetComponent<Actor>();

        if (!GameManager.player) GameManager.player = this;
        else
        {
            StopAllCoroutines();
            DestroyImmediate(gameObject);
        }
    }
    #endregion
}