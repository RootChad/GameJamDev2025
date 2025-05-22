using UnityEngine;

namespace Ded
{
    public class OrientedObject : MonoBehaviour, IPlacable
    {

        [field:SerializeField] public Orientation CurrentOrientation { get; private set; }

        public void Place()
        {

        }
    }
}