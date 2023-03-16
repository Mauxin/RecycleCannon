using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField] TrashType _trashType;

    public TrashType Type => _trashType;
}

public enum TrashType
{
    Organic = 0,
    Plastic = 1,
    Metal = 2
}
