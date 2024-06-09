using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace HF.EditorUtilities.FolderEditor
{
    [CreateAssetMenu(menuName = "FolderTemplate/Create New Template", fileName = "FolderTemplate", order = 0)]
    public class FolderTemplate : ScriptableObject
    {
        [field: SerializeField] public TemplateData templateData { get; set; }

        public FolderTemplate()
        {
        }

        public FolderTemplate(params FolderPack[] Packs)
        {
            templateData = new TemplateData(Packs);
        }
    }

    [Serializable]
    public class TemplateData
    {
        [field: SerializeField] public List<FolderPack> folderPacks { get; private set; } = new List<FolderPack>();

        public bool AddFolder(FolderPack Pack)
        {
            if (folderPacks.Contains(Pack))
                return false;

            folderPacks.Add(Pack);
            return true;
        }

        public bool RemoveFolder(FolderPack Pack)
        {
            if (!folderPacks.Contains(Pack))
                return false;

            folderPacks.Remove(Pack);
            return true;
        }

        [CanBeNull]
        public FolderPack GetFolder(int Index)
        {
            if (Index >= folderPacks.Count)
                return null;

            return folderPacks[Index];
        }

        public TemplateData()
        {
        }

        public TemplateData(params FolderPack[] Packs)
        {
            folderPacks = new List<FolderPack>(Packs);
        }
    }

    [Serializable]
    public class FolderPack
    {
        [field: SerializeField] public string path { get; private set; } = "";
        [field: SerializeField] public FolderPackActionType actionType { get; set; } = FolderPackActionType.Create;
        public List<string> folderNames = new List<string>();

        public bool AddFolder(string Name)
        {
            if (folderNames.Contains(Name))
                return false;

            folderNames.Add(Name);
            return true;
        }

        public bool RemoveFolder(string Name)
        {
            if (!folderNames.Contains(Name))
                return false;

            folderNames.Remove(Name);
            return true;
        }

        public string GetFolder(int Index)
        {
            if (Index >= folderNames.Count)
                return "";

            return folderNames[Index];
        }

        public string GetFolderWithFullPath(int Index)
        {
            if (Index >= folderNames.Count)
                return "";

            return (path + "/" + folderNames[Index]);
        }

        public FolderPack(string Path = "", [NotNull] string[] FolderNames = default,
            FolderPackActionType ActionType = FolderPackActionType.Create)
        {
            if (FolderNames == null) throw new ArgumentNullException(nameof(FolderNames));
            path = Path;
            folderNames = new List<string>(FolderNames);
            actionType = ActionType;
        }
    }
}

public enum FolderPackActionType
{
    Create,
    Delete
}