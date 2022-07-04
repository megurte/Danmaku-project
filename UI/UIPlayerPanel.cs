using TMPro;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class UIPlayerPanel : MonoBehaviour
    {
        public CharacterController player;
    
        public GameObject health;

        public GameObject specials;
    
        [Header("Experience")] public TextMeshProUGUI levelText;
    
        public TextMeshProUGUI experienceText;
    
        [Space(20f)] public TextMeshProUGUI points;
    
        public TextMeshProUGUI score;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            var levelUpMap = player.playerSo.levelUpMap;
        
            levelText.text = "Lv. " + player.level;
            points.text = player.points + "";
            score.text = player.points + "";

            if (player.level + 1 < levelUpMap.keys.Count)
                experienceText.text = player.exp + "/" + levelUpMap.values[player.level];
            else
                experienceText.text = levelUpMap.values[levelUpMap.values.Count - 1] + "/" 
                    + levelUpMap.values[levelUpMap.values.Count - 1];
        }
    }
}
