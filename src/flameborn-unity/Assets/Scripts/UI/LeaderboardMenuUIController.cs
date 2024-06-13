using System;
using System.Collections.Generic;
using Flameborn.UI.Profile;
using PlayFab.ClientModels;
using TMPro;

namespace Flameborn.UI
{

    [Serializable]
    public class LeaderboardMenuUIController
    {
        public LeaderRow Leader = new LeaderRow();
        public List<LeaderRow> Leaders = new List<LeaderRow>();
        public UIRankController UIRankController;

        public void SetLeader(PlayerLeaderboardEntry entity)
        {
            Leader.positionText.text = (entity.Position + 1).ToString();
            Leader.nameText.text = entity.DisplayName ?? "Leader";
            Leader.ratingText.text = entity.StatValue.ToString();
        }

        public void SetLeaderboard(List<PlayerLeaderboardEntry> leaderboard)
        {
            for (int i = 0; i < Leaders.Count; i++)
            {
                if (i < leaderboard.Count)
                {
                    Leaders[i].positionText.text = leaderboard[i].Position.ToString() ?? "~";
                    Leaders[i].nameText.text = leaderboard[i].DisplayName ?? "Unknown user";
                    Leaders[i].ratingText.text = leaderboard[i].StatValue.ToString() ?? "0";
                }
            }
        }

        public void ShowRank()
        {
            UIRankController.Open();
        }

        [Serializable]
        public class LeaderRow
        {
            public TextMeshProUGUI positionText;
            public TextMeshProUGUI nameText;
            public TextMeshProUGUI ratingText;
        }
    }
}