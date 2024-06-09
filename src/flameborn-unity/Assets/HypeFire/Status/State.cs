using System;
using Unity.VisualScripting;

namespace HF.Status
{
    public abstract class State
    {
        protected StateData stateData;
        public StateData GetStateData() => stateData;
        public abstract void OnEnterState(StateMachine stateMachine);
        public abstract void OnUpdateState(StateMachine stateMachine);
        public abstract void OnExitState(StateMachine stateMachine);

        public State(string name, Type stateType)
        {
            stateData = new StateData(name, stateType);
        }

    }

    public struct StateData
    {
        public Type stateType;
        public string name;
        public float lifeTime;
        public DateTime enterTime;
        public DateTime exitTime;
        public StateStatus status;

        public StateData(string name, Type stateType)
        {
            this.stateType = stateType.GetType();
            this.name = name;
            lifeTime = 0f;
            enterTime = DateTime.Now;
            exitTime = DateTime.Now;
            status = StateStatus.Idle;
        }

        public enum StateStatus
        {
            Idle = 0,
            Enter = 1,
            Update = 2,
            Exit = 3
        }
    }
}