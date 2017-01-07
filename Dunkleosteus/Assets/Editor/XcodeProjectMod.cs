using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections;

public class XcodeProjectMod : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS) {
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();

            proj.ReadFromString(File.ReadAllText(projPath));
            string target = proj.TargetGuidByName("Unity-iPhone");

            proj.AddFrameworkToProject(target, "UserNotification.framework", false);

            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}