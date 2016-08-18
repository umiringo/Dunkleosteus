
namespace GlobalDefines
{
	public enum StarState
    {
        Normal = 1,
        Chosen = 2,
        Linked = 3,
    }

    public class PathContainer
    {
        public static readonly string LinkedLinePrefabPath = "Prefabs/LinkedLine";
        public static readonly string LineContainerPath = "LineContainer";
        public static readonly string UIRootPath = "UI Root";

        public static readonly string ContainerPath = "Prefabs/Constellation/";
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
}
