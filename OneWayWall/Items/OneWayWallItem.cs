using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OneWayWall.Items
{
    public class OneWayWallItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.OneWayWall>();
        }

	public override void AddRecipes()
    	    Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
