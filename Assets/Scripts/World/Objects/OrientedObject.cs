using UnityEngine;

namespace Ded
{
    public class OrientedObject : PlaceableObject
    {
        [field:SerializeField] public Orientation CurrentOrientation { get; private set; }
    }
}