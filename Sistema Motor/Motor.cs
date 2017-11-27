using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game.SistemaMotor
{

    public class Motor : MonoBehaviour
    {
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public NavMeshAgent navAgent;
        [HideInInspector]
        public Rigidbody rigidBody;
        [HideInInspector]
        public CharacterController characterController;
        [HideInInspector]
        public Character character;

        [HideInInspector]
        public Vector3 velocidade; //em metros/fixedUpdate
        [HideInInspector]
        public Vector3 velocidadeRotacional; //em angulos/fixedUpdate
        [HideInInspector]
        public Vector3 input;
        [HideInInspector]
        public bool isStarted;

        //public event Action animAcabou;

        public Dictionary<string, IAtivavel> ativaveis = new Dictionary<string, IAtivavel>();

        public MotorEstado estadoAtual;
        [HideInInspector]
        public MotorEstado proximoEstado;
        public MotorEstado defaultEstado;


        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            rigidBody = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();

        } 


        public virtual void MotorStart()
        {
            character = GetComponent<SistemaInventario.ItemHolder>().item.GetComponent<Character>();

            isStarted = true;

            estadoAtual = defaultEstado;
        
            estadoAtual.Construct(this);
        }


        public virtual void FixedUpdate()
        {
            if (isStarted) MotorUpdate();
        }


        protected virtual void MotorUpdate()
        {
            estadoAtual.Transicao(this);
            estadoAtual.ProcessarMovimento(this);
        }


        public virtual bool NoChao() { return false; }


        public void MudarEstado(MotorEstado estado)
        {
            if (estadoAtual != estado)
            {
                estadoAtual.Deconstruct(this);
                estadoAtual = estado;
                estadoAtual.Construct(this);
            }
        }


        public void ProximoEstado()
        {
            if (proximoEstado && estadoAtual != proximoEstado)
                MudarEstado(proximoEstado);

        }


        public void DefaultEstado()
        {
            proximoEstado = defaultEstado;
            MudarEstado(defaultEstado);
        }


        public void AnimacaoEnd()
        {
            estadoAtual.OnAnimacaoEnd(this);
        }

        

        public void Ativar(string aAtivar)
        {
            ativaveis[aAtivar].Ativar(true);
        }

        public void Desativar(string aDesativar)
        {
            ativaveis[aDesativar].Ativar(false);
        }



        public void GetAtivaveisComNome(string n, Action<string> acao)
        {
            foreach (string a in ativaveis.Keys)
            {
                if (a.Contains(n))
                    acao(a);
            }
        }



        public void AtivarTodosComNome(string aAtivar)
        {
            GetAtivaveisComNome(aAtivar, x => ativaveis[x].Ativar(true));
        }



        public void DesativarTodosComNome(string aDesativar)
        {
            GetAtivaveisComNome(aDesativar, x => ativaveis[x].Ativar(false));

        }



        public void ModificarCharacter()
        {
            estadoAtual.ModificarCharacter(this);
        }
    }
}
