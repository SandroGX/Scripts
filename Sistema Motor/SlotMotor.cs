using UnityEngine;
using System.Collections;
using Game.MotorSystem;
using Game.InventorySystem;

[System.Serializable]
public class SlotMotor : Slot, IExterior
{

    protected Motor motor;

    public override void OnDuplicate()
    {
        base.OnDuplicate();
    }

    protected override void OnInsert(Item aPor)
    {
        AoPorExterior(aPor);
    }


    protected virtual void AoPorExterior(Item aPor)
    {
        if (aPor && motor)
        {
            foreach (ItemComponent c in aPor.components)
            {
                IAtivavel a = c as IAtivavel;

                if (a != null)
                    motor.ativaveis.Add(name + a.Name, a);
                
            }
        }
    }



    protected override void AoRetirar(Item aRetirar)
    {
        AoRetirarExterior(aRetirar);
    }


    protected virtual void AoRetirarExterior(Item aRetirar)
    {
        if (aRetirar && motor)
        {
            motor.GetAtivaveisComNome(name, x => motor.ativaveis.Remove(x));
        }
    }



    public virtual void OnCreate()
    {
        motor = item.holder.GetComponent<Motor>();

        AoPorExterior(inserted);

    }

}
