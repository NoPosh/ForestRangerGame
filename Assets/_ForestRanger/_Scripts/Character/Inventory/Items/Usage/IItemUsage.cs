using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IItemUsage
{
    public UniTask Use();
}

public enum ItemUsageType
{
    None,
    Attack
}
