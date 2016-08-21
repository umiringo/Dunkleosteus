
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
    }

    public class PathContainer
    {
        public static readonly string LinkedLinePrefabPath = "Prefabs/LinkedLine";
        public static readonly string LineContainerPath = "LineContainer";
        public static readonly string UIRootPath = "UI Root";

        public static readonly string ContainerPath = "Prefabs/Constellation/";

        public static readonly string YellowCircle = "circle_80_80_f8b711";
        public static readonly string BlueCircle = "circle_80_80_50cce5";
        public static readonly string GreenCircle = "circle_80_80_28ed7b";
        public static readonly string YellowSmallSquare = "square_10_10_f8b711";
        public static readonly string BlueSmallSquare = "square_10_10_50cce5";
        public static readonly string GreenSmallSquare = "square_10_10_28ed7b";
    }

    public class DefineNumber
    {
        public static readonly int LineDepth = 39;
    }
    
    public class ConfigKey
    {
        public static readonly string LevelInfo = "LevelInfo";
        public static readonly string LevelSelect = "LevelSelect";
        public static readonly string Catagory = "Catagory";
    }

    public class PlayerPrefsKey
    {
        public static readonly string LatestLevel = "LatestLevel";
    }

    public class DefineString
    {
        public static readonly string FirstLevel = "Aries";
    }
}
