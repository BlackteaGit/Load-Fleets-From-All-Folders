using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using CoOpSpRpG;
using System.Reflection;
using WTFModLoader;
using WTFModLoader.Manager;


namespace LoadFleetsFromAllFolders
{
    public class LoadFleetsFromAllFolders : IWTFMod
    {

        public ModLoadPriority Priority => ModLoadPriority.Normal;
        public void Initialize()
        {
            Harmony harmony = new Harmony("blacktea.LoadFleetsFromAllFolders");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(DATA_FOLDER), "init")]
        public class DATA_FOLDER_init
        {
            [HarmonyPostfix]
            private static void Postfix(ref List<string> ___folders)
            {
                String SteamModsDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), System.IO.Path.Combine(@"..\..\workshop\content\392080")));
                String ModsDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), System.IO.Path.Combine(@"Mods")));
                List<string> MyFolders = new List<string>();

                if (System.IO.Directory.Exists(SteamModsDirectory))
                {
                    var dirs = from dir in
                     System.IO.Directory.EnumerateDirectories(SteamModsDirectory, "*",
                        System.IO.SearchOption.AllDirectories)
                               select dir;

                    foreach (var dir in dirs)
                    {
                        ___folders.Add(dir);
                    }
                }

                if (System.IO.Directory.Exists(ModsDirectory))
                {
                    var dirs = from dir in
                     System.IO.Directory.EnumerateDirectories(ModsDirectory, "*",
                        System.IO.SearchOption.AllDirectories)
                               select dir;

                    foreach (var dir in dirs)
                    {
                        ___folders.Add(dir);
                    }
                }

                ___folders = ___folders.Distinct().ToList();
                foreach (var entry in ___folders)
                {
                    if (System.IO.Directory.Exists(entry))
                    {
                        MyFolders.Add(entry);
                    }

                }
                ___folders = MyFolders;
            }
        }
    }
}
