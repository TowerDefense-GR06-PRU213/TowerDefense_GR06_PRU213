using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Objects/HeroData")]
public class HeroData_Lv3 : ScriptableObject
{
   /*  public float range;
     public float shootInterval;
     public float projectileSpeed;
     public float projectileDuration;
     public float damage;*/

    [Header("Stats")]
    public float range = 5f;
    public float shootInterval = 1f;
    public float projectileSpeed = 8f;
    public float projectileDuration = 3f;
    public float damage = 10f;

    [Header("Targeting")]
    public TargetPriority targetPriority = TargetPriority.First; // Ưu tiên bắn ai

    [Header("Visual & Audio")]
    public GameObject projectilePrefab;  // Prefab viên đạn riêng cho hero
    public AudioClip shootSound;         // Âm thanh bắn (nếu có)

    [Header("Cost & Upgrade")]
    public int cost = 100;               // Giá đặt hero
    public int upgradeCost = 50;         // Giá nâng cấp hero
    public int maxLevel = 3;             // Level tối đa
    public Sprite sprite;

    [Header("Upgrade Stats Multiplier")]
    [Tooltip("Mỗi level tăng damage thêm % này")]
    public float damageUpgradeMultiplier = 0.2f;     // +20% damage/level
    [Tooltip("Mỗi level tăng range thêm % này")]
    public float rangeUpgradeMultiplier = 0.1f;      // +10% range/level
    [Tooltip("Mỗi level giảm shoot interval % này (bắn nhanh hơn)")]
    public float shootSpeedUpgradeMultiplier = 0.15f; // -15% interval/level

    [Header("Special Buff")]
    public bool isMapIceHero = false;
    public float iceDamageMultiplier = 1.3f;
    public float iceAttackSpeedMultiplier = 1.25f;

    public GameObject prefab;
}

