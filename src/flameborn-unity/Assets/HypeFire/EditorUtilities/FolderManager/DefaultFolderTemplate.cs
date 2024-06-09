using HF.Logger;
using UnityEngine;

namespace HF.EditorUtilities.FolderEditor
{
    [CreateAssetMenu(menuName = "FolderTemplate/Create Default Template", fileName = "DefaultFolderTemplate",
        order = 1)]
    public class DefaultFolderTemplate : FolderTemplate
    {
        private bool _isBuilded;
        public static FolderTemplate GlobalAccess { get; private set; }

        public DefaultFolderTemplate() : base()
        {
            GlobalAccess = this;
        }

        private void OnEnable()
        {
            if (!_isBuilded)
                Build();
        }

        public void Build()
        {
            var foldersDelete = new string[] { "TutorialInfo" };
            var foldersCreate = new string[]
            {
                "Art", "Art/Materials", "Art/Models", "Art/VFXs", "Art/Prefabs",
                "Art/2Ds", "Art/Textures", "Art/Animations", "Models", "Prefabs","Scripts"
            };

            var delete = new FolderPack(Application.dataPath, foldersDelete,
                FolderPackActionType.Delete);

            var create = new FolderPack(Application.dataPath, foldersCreate,
                FolderPackActionType.Create);

            templateData = new TemplateData(delete, create);
            _isBuilded = true;
        }
    }
}