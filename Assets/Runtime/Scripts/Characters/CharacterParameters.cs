using UnityEngine;

[CreateAssetMenu(menuName = "Data/Characters/Parameters")]
public class CharacterParameters : ScriptableObject
{
    [field: SerializeField]
    public float RotationAcc { get; private set; } = 20.0f;

    [field: SerializeField]
    public float MovementSpeed { get; private set; } = 10.0f;

    [field: SerializeField]
    public float MovementAcc { get; private set; } = 100.0f;
}
