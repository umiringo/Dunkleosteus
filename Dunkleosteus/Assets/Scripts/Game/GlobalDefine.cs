
namespace GlobalDefines
{
	public enum StarState
    {
        Normal = 1,
        Chosen = 2,
        Linked = 3,
    }

    public enum LevelState
    {
        Finished = 1,
        Current = 2,
        Unabled = 3,
        Spot = 4,
    }

    public class PathContainer
    {
        public static readonly string LinkedLinePrefabPath = "Prefabs/StarLine/LinkedLine";
        public static readonly string CardLinkedLinePrefabPath = "Prefabs/StarLine/CardLinkedLine";
        public static readonly string CardInfoPrefabPath = "Prefabs/CardView/CardInfoTemplate";
        public static readonly string LineContainerPath = "LineContainer";
        public static readonly string UIRootPath = "UI Root";

        public static readonly string ContainerPath = "Prefabs/Constellation/";
        public static readonly string CatagoryPath = "Prefabs/LevelContainer/";
        public static readonly string CardPath = "Prefabs/CardView/CardPreview/";

        public static readonly string YellowCircle = "circle_80_80_f8b711";
        public static readonly string BlueCircle = "circle_80_80_50cce5";
        public static readonly string GreenCircle = "circle_80_80_28ed7b";
        public static readonly string YellowSmallSquare = "square_10_10_f8b711";
        public static readonly string BlueSmallSquare = "square_10_10_50cce5";
        public static readonly string GreenSmallSquare = "square_10_10_28ed7b";
        public static readonly string YellowLargeCircle = "100_100_f8b711_circle";
        public static readonly string BlueLargeCircle = "100_100_50cce5 circle";

        public static readonly string SpotSquare = "square_256_256_353c44";

        public static readonly string normalBlueSquare = "80_80_square_5_ 50cce5";
        public static readonly string darkBlueSquare = "80_80_square_5_ 465F6C";
    }

    public class DefineNumber
    {
        public static readonly int LineDepth = 39;
        public static readonly int TipCost = 1;
        public static readonly int DuplicateStarNum = 6;
        public static readonly int LevelSelectStarThreshold = 10;
        public static readonly int LittleStarCount = 28;
        public static readonly int WinBonusNum = 1;
        public static readonly int CardLineDepth = 399;

    }
    
    public class ConfigKey
    {
        public static readonly string LevelInfo = "LevelInfo";
        public static readonly string LevelSelect = "LevelSelect";
        public static readonly string Catagory = "Catagory";
        public static readonly string LocalizeJson = "Localize";
    }

    public class PlayerPrefsKey
    {
        public static readonly string LatestLevel = "LatestLevel";
        public static readonly string Coin = "Coin";
        public static readonly string Language = "Language";
        public static readonly string Music = "Music";
        public static readonly string Sound = "Sound";
        public static readonly string Sale12 = "Sale12";
    }

    public class DefineString
    {
        public static readonly string FirstLevel = "Aries";
        public static readonly string DefaultCatagory = "Zodiac";
        public static readonly string DarkBlueColor = "[465F6C]";
        public static readonly string NormalBlueColor = "[50CCE5]";
    }

    public class DefinePurchaseId
    {
        public static readonly string PurchaseId10 = "constellationpainter.umiringo.com.purchase.10";
        public static readonly string PurchaseId40 = "constellationpainter.umiringo.com.purchase.40";
        public static readonly string PurchaseId160 = "constellationpainter.umiringo.com.purchase.160";
        public static readonly string PurchaseId360 = "constellationpainter.umiringo.com.purchase.360";
        public static readonly string PurchaseIdSale12 = "constellationpainter.umiringo.com.purchase.sale.12";
    }

    public class LocalizeStringKey
    {
        public static readonly string NotificationMessage = "NotificationMessage";
    }
}
