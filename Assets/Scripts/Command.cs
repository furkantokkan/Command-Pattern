using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public abstract class Command
    {
        public abstract void Execute(Animator anim, bool forward);
    }

    public class MoveForward : Command
    {
        public override void Execute(Animator anim, bool forward)
        {
            if (forward)
                anim.SetTrigger("Walk");
            else
                anim.SetTrigger("WalkR");
        }
    }

public class PerfomAttack : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("Attack");
        else
            anim.SetTrigger("AttackR");
    }
}

public class PerfomPassive : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("Passive");
        else
            anim.SetTrigger("PassiveR");
    }
}