using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class MeleeAttackUsage : IItemUsage
{
    private Transform _transform;
    private readonly float _attackDistance = 1f;
    private readonly Vector2 _boxSize = new Vector2(2f, 1f);

    private InventoryItemData _inventoryItemData;

    public MeleeAttackUsage(Transform transform, InventoryItemData itemData)
    {
        _transform = transform;
        _inventoryItemData = itemData;
    }

    public async UniTask Use()
    {
        Vector2 attackPoint = (Vector2)_transform.position + (Vector2)_transform.up * _attackDistance;

        // Получаем угол поворота персонажа для коробки
        float angle = _transform.eulerAngles.z;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint, _boxSize, angle);
        DrawDebugBox(attackPoint, _boxSize, angle);
        
        foreach (var enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_inventoryItemData.Damage); // Урон можно брать из данных предмета
            }
        }
        
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
    }

    private void DrawDebugBox(Vector2 center, Vector2 size, float angle)
    {
        float halfWidth = size.x / 2f;
        float halfHeight = size.y / 2f;

        // Вычисляем углы локально
        Vector2[] corners = new Vector2[]
        {
        new Vector2(-halfWidth, -halfHeight),
        new Vector2(halfWidth, -halfHeight),
        new Vector2(halfWidth, halfHeight),
        new Vector2(-halfWidth, halfHeight)
        };

        // Поворачиваем углы и переносим в мировые координаты
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        for (int i = 0; i < 4; i++)
        {
            corners[i] = (Vector2)(rotation * corners[i]) + center;
        }

        // Рисуем линии между углами
        Debug.DrawLine(corners[0], corners[1], Color.red, 0.5f);
        Debug.DrawLine(corners[1], corners[2], Color.red, 0.5f);
        Debug.DrawLine(corners[2], corners[3], Color.red, 0.5f);
        Debug.DrawLine(corners[3], corners[0], Color.red, 0.5f);
    }
}
