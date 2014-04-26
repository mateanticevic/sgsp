using SGSP.eAdventure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.UnityProject
{
    public class UnityProjectUtility
    {
        public static void CreateUnityProject(Chapter chapter, string xmlRootPath)
        {
            string sceneDirPath = "UnityProject/Assets/Scenes";
            string resDirPath = "UnityProject/Assets/Resources";
            string assetsDirPath = resDirPath + "/assets";
            string eAdventureAssets = Path.GetDirectoryName(xmlRootPath) + "\\assets";

            if (Directory.Exists(sceneDirPath)) Directory.Delete(sceneDirPath, true);
            if (Directory.Exists(resDirPath)) Directory.Delete(resDirPath, true);

            Directory.CreateDirectory(sceneDirPath);
            Directory.CreateDirectory(resDirPath);

            Directory.CreateDirectory(assetsDirPath);

            foreach (string dirPath in Directory.GetDirectories(eAdventureAssets, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(eAdventureAssets, assetsDirPath));

            foreach (string newPath in Directory.GetFiles(eAdventureAssets, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(eAdventureAssets, assetsDirPath), true);

            foreach (var scene in chapter.Scenes)
            {
                var defScene = "UnityProject/Default.unity";

                File.Copy(defScene, "UnityProject/Assets/Scenes/" + scene.Id + ".unity");
            }

            foreach (var item in Directory.GetFiles("UnityScripts"))
            {
                File.Copy(item, "UnityProject/Assets/" + Path.GetFileName(item), true);
            }


        }
    }
}
