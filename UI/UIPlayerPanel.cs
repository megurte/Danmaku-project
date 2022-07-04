using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        [Header("Prefabs")] public GameObject iconHealthFull;
        
        public GameObject iconHealthEmpty;
        
        public GameObject iconSpecialFull;
        
        public GameObject iconSpecialEmpty;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            ClearFiller(health);
            ClearFiller(specials);
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

            UpdateHealthFiller();
            UpdateSpecialFiller();
        }

        private void ClearFiller(GameObject filler) {
            var allChildren = new GameObject[filler.transform.childCount];
            var i = 0;

            foreach (Transform child in filler.transform)
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }

            foreach (var child in allChildren)
            {
                Destroy(child.gameObject);
            }
        }

        private void UpdateHealthFiller()
        {
            if (health.transform.childCount >= 7)
                return;

            for (var i = 0; i < player.health; i++)
            {
                var prefab = Instantiate(iconHealthFull);
                prefab.transform.SetParent(health.transform);
            }
            
            for (var i = 0; i < player.maxHealth - player.health; i++)
            {
                var prefab = Instantiate(iconHealthEmpty);
                prefab.transform.SetParent(health.transform);
            }
        }
        
        private void UpdateSpecialFiller()
        {
            if (specials.transform.childCount >= 7)
                return;

            for (var i = 0; i < player.special; i++)
            {
                var prefab = Instantiate(iconSpecialFull);
                prefab.transform.SetParent(specials.transform);
            }
            
            for (var i = 0; i < player.maxValue - player.special; i++)
            {
                var prefab = Instantiate(iconSpecialEmpty);
                prefab.transform.SetParent(specials.transform);
            }
        }
    }
}
