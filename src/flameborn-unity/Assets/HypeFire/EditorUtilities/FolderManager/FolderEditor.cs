using HF.EditorUtilities.Folder;
using HF.Logger;
using UnityEditor;

namespace HF.EditorUtilities.FolderEditor
{
#if UNITY_EDITOR

    public static class FolderEditor
    {
        public static void Build(FolderTemplate Template)
        {
        }

        [MenuItem("Folder Manager/Build Default Template")]
        public static void BuildDefaultTemplate()
        {
            if (DefaultFolderTemplate.GlobalAccess is null)
            {
                HFLogger.LogError(DefaultFolderTemplate.GlobalAccess, "is not Exists.");
                return;
            }

            var template = DefaultFolderTemplate.GlobalAccess.templateData;
            foreach (var pack in template.folderPacks)
            {
                foreach (var folderName in pack.folderNames)
                {
                    FolderHelper helper =
                        FolderHelper.GetNewFolderHelper(new FolderHelper.FolderData(folderName), true);

                    if (pack.actionType == FolderPackActionType.Delete)
                    {
                        helper.Delete();
                        continue;
                    }

                    helper.Create();
                }
            }
        }
    }

#endif
}