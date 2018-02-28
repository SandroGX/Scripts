using UnityEngine;
using System.Collections;
using Game.MotorSystem;
using Game.InventorySystem;

[System.Serializable]
public class SlotMotor : Slot, IExterior
{

    protected Motor motor;

    protected override void OnInsert(Item aPor)
    {
        SetExterior(aPor);
    }


    protected virtual void SetExterior(Item toSet)
    {
        if (toSet && motor)
        {
            foreach (ItemComponent c in toSet.components)
            {
                IActivatable a = c as IActivatable;

                if (a != null) motor.activatables.Add(name + a.Name, a);
            }
        }
    }



    protected override void OnWithDraw(Item aRetirar)
    {
        ResetExterior(aRetirar);
    }


    protected virtual void ResetExterior(Item aRetirar)
    {
        if (aRetirar && motor)
            motor.GetAtivatablesWithName(name, x => motor.activatables.Remove(x));
        
    }


    public virtual void OnCreate()
    {
        motor = item.holder.GetComponent<Motor>();

        SetExterior(inserted);
    }

}
