public class WeaponShopElement : ShopElement
{
    private readonly string Name = "WeaponShop";

    public override bool IsEquip
    {
        get => Game.Locator.SkinView.EquipWeapon == _id;
        set
        {
            Game.Locator.SkinView.EquipWeapon = _id;
        }
    }

    protected override string SaveName => Name;

    protected override void Start()
    {
        base.Start();
        Game.Locator.SkinView.OnSwapWeapon += UpdateUI;
    }
}