using UnityEngine;
using System;

public enum TickType { Update, FixedUpdate, Seconds, SecondsRealtime }

public class GenericYieldInstructionGetter
{
    public TickType tickType;
    public float tick;
    private object yield;

    public object GetYieldInstruction()
    {
        if(yield == null)
            switch (tickType)
            {
                case TickType.Update: yield = null; break;
                case TickType.FixedUpdate: yield = new WaitForFixedUpdate(); break;
                case TickType.Seconds: yield = new WaitForSeconds(tick); break;
                case TickType.SecondsRealtime: yield = new WaitForSecondsRealtime(tick); break;
            } 

        return yield;
    }
}
