using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    Animator anim;
    Command keySpace, KeyQ, KeyW;
    List<Command> oldCommands = new List<Command>();

    Coroutine replayCoroutine;
    bool ShouldStartReplay;
    bool isReplaying;
    void Start()
    {
        keySpace = new PerfomAttack();
        KeyQ = new PerfomPassive();
        KeyW = new PerfomAttack();
        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

    void Update()
    {
        if (!isReplaying)
        {
            HandleInput();
        }
        StartReplay();

    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(anim, true);
            oldCommands.Add(keySpace);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            KeyQ.Execute(anim, true);
            oldCommands.Add(KeyQ);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            KeyW.Execute(anim, true);
            oldCommands.Add(KeyW);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShouldStartReplay = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
    }
    void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command c = oldCommands[oldCommands.Count - 1];
            c.Execute(anim, false);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }
    void StartReplay()
    {
        if (ShouldStartReplay && oldCommands.Count > 0)
        {
            ShouldStartReplay = false;
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }
    IEnumerator ReplayCommands()
    {
        isReplaying = true;

        for (int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(anim, true);
            yield return new WaitForSeconds(1f);
        }

        isReplaying = false;
    }

}
