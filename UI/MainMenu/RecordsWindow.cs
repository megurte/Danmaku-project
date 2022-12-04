using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class RecordsWindow: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateRecordsData()
        {
            var loadedJsonData = JsonScoreDataWriter.LoadJsonData();

            if (loadedJsonData is null) return;

            dateText.text = "";
            nameText.text = "";
            scoreText.text = "";

            foreach (var dataItem in loadedJsonData)
            {
                dateText.text += dataItem.date + "\n";
                nameText.text += dataItem.name + "\n";
                scoreText.text += dataItem.score + "\n";
            }
        }
    }
}