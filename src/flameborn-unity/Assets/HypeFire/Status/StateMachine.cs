namespace HF.Status
{
    using System.Collections.Generic;
    using HF.Extensions;
    using UnityEngine;
    using UnityEngine.Events;

    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private List<State> m_StateHistory = new List<State>();
        [SerializeField] private State m_CurrentState;

        public UnityEvent<State> OnStateEvent { get; private set; } = new UnityEvent<State>();

#nullable enable
        public State? CurrentState => m_CurrentState;
#nullable disable        

        protected virtual void Update()
        {
            if (CurrentState.IsNotNull())
            {
                CurrentState.OnUpdateState(this);
            }
        }

        public void Subscribe(UnityAction<State> stateListener)
        {
            OnStateEvent.AddListener(stateListener);

            if (CurrentState.IsNotNull())
                stateListener.Invoke(CurrentState);
        }

        public virtual void SwitchState(State newState)
        {
            if (m_CurrentState.IsNotNull())
            {
                m_CurrentState.OnExitState(this);
                m_CurrentState = null;
            }

            newState.OnEnterState(this);
            m_StateHistory.Add(newState);
            m_CurrentState = newState;
        }
    }
}