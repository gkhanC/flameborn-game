using System;
using HF.Extensions;
using HF.Logger;
using UnityEngine;

namespace HF.EditorUtilities.Folder
{
#if UNITY_EDITOR


    public class FolderHelper : IFolderHelper
    {
        private FolderData _data;
        private bool _isValidated;
        private bool _is_logger_enable;
        public bool isLoggerEnabled => _is_logger_enable;
        public string name => _data.name;

        public bool isValidated => _isValidated;

        /// <summary>
        /// Yeni dosya yaratır.
        /// </summary>
        /// <returns>Dosya yaratımı başarılı ise true döndürür.</returns>
        public bool Create()
        {
            if (IsValidate(false))
            {
                if (_is_logger_enable)
                {
                    var p = Application.dataPath;
                    this.LogWarning(
                        $"Could not be created folder \"{name.GetWithColor("green")}\", The Path already has folder \"{name.GetWithColor("green")}\". Path: {p.GetWithColor("green")}");
                }

                return false;
            }

            var result = FolderManager.Create(this);
            return result;
        }

        /// <summary>
        /// Mevcut dosyayı siler.
        /// <returns>Dosya silme işlemi başarılı ise true döndürür.</returns>
        /// </summary>
        /// 
        public bool Delete()
        {
            if (!IsValidate(false))
            {
                if (_is_logger_enable)
                {
                    var p = Application.dataPath;
                    this.LogWarning(
                        $"Could not be deleted folder \"{name.GetWithColor("green")}\", The Path doesn't has folder \"{name.GetWithColor("green")}\". Path: {p.GetWithColor("green")}");
                }

                return false;
            }

            var result = FolderManager.Delete(this);
            return result;
        }

        private FolderHelper(FolderData Data, bool isLoggerEnable)
        {
            _is_logger_enable = isLoggerEnable;
            _data = Data;
        }

        /// <summary>
        /// Helper'da belirtilen dosya ve path'in varlığını doğrular.
        /// </summary>
        /// <param name="isLoggerEnable"></param>
        /// <returns>Dosya mevcur ise true döndürür.</returns>
        public bool IsValidate(bool isLoggerEnable = true)
        {
            if (FolderManager.IsContains(this))
            {
                _isValidated = true;

                if (isLoggerEnable)
                {
                    var p = Application.dataPath;
                    this.LogValidate(
                        $"Folder \"{name.GetWithColor("green")}\" is found. Path: {p.GetWithColor("green")}");
                }

                return _isValidated;
            }


            if (isLoggerEnable)
            {
                var p = Application.dataPath;
                this.LogWarning(
                    $"Folder \"{name.GetWithColor("green")}\" is not found. Path: {p.GetWithColor("green")}");
            }

            _isValidated = false;
            return _isValidated;
        }

        public static FolderHelper GetNewFolderHelper(FolderData Data, bool isLoggerEnable = true)
        {
            var result = new FolderHelper(Data, isLoggerEnable);
            result.IsValidate();

            return result;
        }

        public static FolderHelper GetNewFolderHelper(string Name = "NoNameFolder", string Path = "",
            bool isLoggerEnable = true)
        {
            var result = new FolderHelper(new FolderData(Name), isLoggerEnable);
            result.IsValidate();

            return result;
        }

        [Serializable]
        public struct FolderData
        {
            public string name;


            public FolderData(FolderData data)
            {
                name = data.name;
            }

            public FolderData(string Name = "NoNameFolder")
            {
                name = Name;
            }
        }
    }

#endif
}