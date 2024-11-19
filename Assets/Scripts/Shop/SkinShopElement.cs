public class SkinShopElement : ShopElement
{
    private readonly string Name = "SkinShop";

    public override bool IsEquip 
    { 
        get => Game.Locator.SkinView.EquipSkin == _id;
        set 
        {
            Game.Locator.SkinView.EquipSkin = _id;
        }
    }

    protected override string SaveName => Name;

    protected override void Start()
    {
        base.Start();
        Game.Locator.SkinView.OnSwapSkin += UpdateUI;
    }
}