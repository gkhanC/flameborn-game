using flameborn.Core.Game.Events;
using flameborn.Core.Game.Objects.Abstract;
using UnityEngine;
namespace flameborn.Core.Game
{

    public class WoodPile : MonoBehaviour, ISelectable
    {
        public SelectableTypes selectableType => SelectableTypes.Prob;

        public GameObject GetGameObject => this.gameObject;

        public void DeSelect()
        {

        }

        public void Select()
        {

        }

        public void SetDestination(Vector3 position)
        {

        }

        public void SetTarget(ISelectable go)
        {

        }
    }
}