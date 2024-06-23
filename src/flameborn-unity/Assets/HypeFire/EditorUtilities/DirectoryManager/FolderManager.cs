using System;
using System.Linq;
using HF.Extensions;
using HF.Logger;
using UnityEditor;
using UnityEngine;

namespace HF.EditorUtilities.Folder
{
    internal static class FolderManager
    {
#if UNITY_EDITOR


        /// <summary>
        /// Helper ile belirtilen dosyayı yaratır.
        /// </summary>
        /// <param name="Helper"></param>
        /// <returns>Dosya yaratımı başarılı ise true döndürülür.</returns>
        internal static bool Create(FolderHelper Helper)
        {
            var path = Application.dataPath + "/" + Helper.name;

            if (UnityEngine.Windows.Directory.Exists(path))
            {
                Helper.LogWarning($"The folder exists already", path);
                return false;
            }

            var pathOrders = (Helper.name).Split("/").ToList();

            var currentPath = Application.dataPath;

            foreach (var VARIABLE in pathOrders)
            {
                currentPath += "/" + VARIABLE;
                try
                {
                    currentPath.Log();
                    UnityEngine.Windows.Directory.CreateDirectory(currentPath);
                }
                catch (Exception e)
                {
                    Helper.LogError(Helper, e);
                    return false;
                }
            }

            Helper.LogSuccess($"{Helper.name.GetWithColor("green")} New Directory is created", path);
            AssetDatabase.Refresh();
            return true;
        }

        /// <summary>
        /// Helper ile belirtilen dosyayı siler.
        /// </summary>
        /// <param name="Helper"></param>
        /// <returns>Dosya silme işlemi başarılı ise true döndürür.</returns>
        internal static bool Delete(FolderHelper Helper)
        {
            var path = Application.dataPath + "/" + Helper.name;

            try
            {
                if (UnityEngine.Windows.Directory.Exists(path))
                {
                    FileUtil.DeleteFileOrDirectory(path);
                    FileUtil.DeleteFileOrDirectory(path + ".meta");

                    Helper.LogSuccess("The Folder is deleted", path);
                    AssetDatabase.Refresh();
                    return true;
                }

                Helper.LogSuccess("The folder doesn't exist.", path);
                return false;
            }
            catch (Exception e)
            {
                Helper.LogError(Helper, e);
                return false;
            }
        }

        internal static bool Move(FolderHelper Helper, string TargetPath)
        {
            var path = Application.dataPath + "/" + Helper.name;

            try
            {
                if (!UnityEngine.Windows.Directory.Exists(path))
                {
                    Helper.LogWarning($"The folder doesn't exists", path);
                    return false;
                }

                if (!UnityEngine.Windows.Directory.Exists(TargetPath))
                {
                    Helper.LogWarning($"The folder doesn't exists", TargetPath);
                    return false;
                }

                FileUtil.MoveFileOrDirectory(path, TargetPath);
                Helper.LogSuccess($"{Helper.name.GetWithColor("green")} New Directory is moved", TargetPath);
                AssetDatabase.Refresh();
                return true;
            }
            catch (Exception e)
            {
                Helper.LogError(Helper, e);
                return false;
            }
        }

        /// <summary>
        /// Helper ile belirtilen dosya mevcut mu kontrol eder.
        /// </summary>
        /// <param name="Helper"></param>
        /// <returns>Dosya mevcur ise true döndürür.</returns>
        internal static bool IsContains(FolderHelper Helper)
        {
            var path = Application.dataPath + "/" + Helper.name;

            if (UnityEngine.Windows.Directory.Exists(path))
            {
                if (Helper.isLoggerEnabled)
                    Helper.LogSuccess("Validation of  Exists", $"The folder exists already", path);
                return true;
            }

            if (Helper.isLoggerEnabled)
                Helper.LogWarning("Validation of  Exists", $"The folder doesn't exist", path);
            return false;
        }
#endif
    }
}