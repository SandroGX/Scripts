using UnityEngine;
using System.Collections;
using Game.SistemaMotor;
using Game.SistemaInventario;

[System.Serializable]
public class SlotMotor : Slot, IExterior
{

    protected Motor motor;

    public override void AoDuplicar()
    {
        base.AoDuplicar();
    }

    protected override void AoPor(Item aPor)
    {
        AoPorExterior(aPor);
    }


    protected virtual void AoPorExterior(Item aPor)
    {
        if (aPor && motor)
        {
            foreach (ItemComponent c in aPor.componentes)
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



    public virtual void OnCriado()
    {
        motor = item.holder.GetComponent<Motor>();

        AoPorExterior(guardado);

    }

}
