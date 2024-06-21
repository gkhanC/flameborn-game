using System;
using UnityEngine;

namespace flameborn.Core.Game.Inputs.Controllers
{
    [Serializable]
    public class TouchResult
    {
        public InputStatus status;
        public Vector3 startPosition;
        public Vector3 endPosition;
        public GameObject selectedObject;
    }

    public enum InputStatus
    {
        None,
        Touched,
        Continuos,
        TouchCompleted,
        ObjectSelect
    }
}