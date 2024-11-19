using Spine.Unity;
using System;
using UnityEngine;

public class SkinView : MonoBehaviour
{
    public Action OnSwapSkin;
    public Action OnSwapWeapon;

    [SerializeField] private SkeletonAnimation _skeleton;
    [SerializeField] private SpriteRenderer _weaponSkin;

    [SerializeField] private string[] _animationName;
    [SerializeField] private string[] _skinName;
    [SerializeField] private Sprite[] _weaponSprites;

    public int EquipSkin
    {
        get => PlayerPrefs.GetInt("Skin", 0);
        set
        {
            PlayerPrefs.SetInt("Skin", value);
            _skeleton.Skeleton.SetSkin(_skinName[value]);
            OnSwapSkin?.Invoke();
        }
    }

    public int EquipWeapon
    {
        get => PlayerPrefs.GetInt("Weapon", 0);
        set
        {
            PlayerPrefs.SetInt("Weapon", value);
            _weaponSkin.sprite = _weaponSprites[value];
            OnSwapWeapon?.Invoke();
        }
    }

    private void Start()
    {
        _skeleton.Skeleton.SetSkin(_skinName[EquipSkin]);
        _weaponSkin.sprite = _weaponSprites[EquipWeapon];
    }

    public void Play(int value)
    {
        _skeleton.AnimationState.SetAnimation(0, _animationName[value], value == 1);
    }
}