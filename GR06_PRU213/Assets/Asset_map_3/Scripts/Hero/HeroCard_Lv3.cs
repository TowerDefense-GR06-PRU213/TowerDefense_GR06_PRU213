using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroCard_Lv3 : MonoBehaviour
{
    [SerializeField] private Image heroImage;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text nameText; // Thêm field cho tên

    private HeroData_Lv3 _heroData;
    public static event Action<HeroData_Lv3> OnHeroSelected;

    public void Initialize(HeroData_Lv3 data)
    {
        _heroData = data;
        heroImage.sprite = data.sprite;
        costText.text = data.cost.ToString();
        
        // Hiển thị tên (lấy từ asset name và bỏ ký tự đặc biệt)
        if (nameText != null)
        {
            string cleanName = data.name.Replace("_", " ");
            nameText.text = cleanName;
        }
    }

    public void PlaceTower()
    {
        OnHeroSelected?.Invoke(_heroData);
    }

}
