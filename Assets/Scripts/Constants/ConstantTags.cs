using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Constants
    {
        [System.Serializable]
            public enum TagType
            {
                NONE = 0,
                APPLICATION,
                GAME,
                UI
            }
            public enum ApplicationTag
            {
                NONE = 0,
                BASE_APP,
                DISPLAY,
                WINDOW,
                SUB_WINDOW,
                INPUT,
                NETWORK
            }
            public enum GameTag
            {
                NONE = 0,
                PLAYER,
                PAWN,
                ENEMY,
                AI,
                CHARACTER,
                FOLIAGE,
                INTERACTABLE,
                WATER,
                FLOOR,
                CEILING,
                WALL, 
                ENVIRONMENT
            }
            public enum UITag
            {
                NONE = 0,
                BUTTON,
                COLOR_PICKER,
                HUD,
                HUD_COMPONENT,
                CURSOR,
                TEXT_INPUT,
                NUMERICAL_INPUT
            }
        public class ConstTag
        {

            private List<TagType> tagTypes = new List<TagType>();
            private List<ApplicationTag> appTags = new List<ApplicationTag>();
            private List<GameTag> gameTags = new List<GameTag>();
            private List<UITag> uiTags = new List<UITag>();

            ConstTag()
            {
                tagTypes.Add(TagType.NONE);
                appTags.Add(ApplicationTag.NONE);
                gameTags.Add(GameTag.NONE);
                uiTags.Add(UITag.NONE);
            }

            public ConstTag(TagType tagType, ApplicationTag appTag = ApplicationTag.NONE, GameTag gameTag = GameTag.NONE, UITag uiTag = UITag.NONE)
            {
                tagTypes.Add(tagType);
                appTags.Add(appTag);
                gameTags.Add(gameTag);
                uiTags.Add(uiTag);
            }

            // TagType
            public void AddTagType(TagType tagType)
            {
                if (!tagTypes.Contains(tagType))
                    tagTypes.Add(tagType);
            }
            public void RemoveTagType(TagType tagType)
            {
                tagTypes.Remove(tagType);
            }

            // ApplicationTag
            public void AddAppTag(ApplicationTag tag)
            {
                if (!appTags.Contains(tag))
                    appTags.Add(tag);
            }
            public void RemoveAppTag(ApplicationTag tag)
            {
                appTags.Remove(tag);
            }

            // GameTag
            public void AddGameTag(GameTag tag)
            {
                if (!gameTags.Contains(tag))
                    gameTags.Add(tag);
            }
            public void RemoveGameTag(GameTag tag)
            {
                gameTags.Remove(tag);
            }

            // UITag
            public void AddUITag(UITag tag)
            {
                if (!uiTags.Contains(tag))
                    uiTags.Add(tag);
            }
            public void RemoveUITag(UITag tag)
            {
                uiTags.Remove(tag);
            }



            public bool HasTagType(TagType type) => tagTypes.Contains(type);
            public bool HasAppTag(ApplicationTag tag) => appTags.Contains(tag);
            public bool HasGameTag(GameTag tag) => gameTags.Contains(tag);
            public bool HasUITag(UITag tag) => uiTags.Contains(tag);



            public IReadOnlyList<TagType> GetTagTypes() => tagTypes.AsReadOnly();
            public IReadOnlyList<ApplicationTag> GetAppTags() => appTags.AsReadOnly();
            public IReadOnlyList<GameTag> GetGameTags() => gameTags.AsReadOnly();
            public IReadOnlyList<UITag> GetUITags() => uiTags.AsReadOnly();

        }
    }
}