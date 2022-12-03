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
            var newData = JsonDataWriter.LoadJsonData();
            
            foreach (var dataItem in newData)
            {
                dateText.text += dataItem.date + "\n";
                nameText.text += dataItem.name + "\n";
                scoreText.text += dataItem.score + "\n";
            }
        }
    }
}