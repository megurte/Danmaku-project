using Character;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Scene.Player
{
    public class UIPlayerPanel : MonoBehaviour
    {
        public PlayerModel player;
    
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

        [Inject]
        public void Construct(PlayerModel settings)
        {
            player = settings;
        }
        
        private void Start()
        {
            UpdateHealthFiller(player.playerSo.health);
            UpdateSpecialFiller(player.playerSo.special);
            
            GlobalEvents.OnHealthChange.AddListener(UpdateHealthFiller);
            GlobalEvents.OnSpecialChange.AddListener(UpdateSpecialFiller);
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

        private static void ClearFiller(GameObject filler) {
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

        private void UpdateHealthFiller(int currentHealth)
        {
            if (currentHealth < 0 || currentHealth > player.playerSo.maxHealth)
                return;
            
            ClearFiller(health);

            for (var i = 0; i < currentHealth; i++)
            {
                var prefab = Instantiate(iconHealthFull);
                prefab.transform.SetParent(health.transform);
                SetNormalScale(prefab);
            }
            
            for (var i = 0; i < player.playerSo.maxHealth - currentHealth; i++)
            {
                var prefab = Instantiate(iconHealthEmpty);
                prefab.transform.SetParent(health.transform);
                SetNormalScale(prefab);
            }
        }
        
        private void UpdateSpecialFiller(int currentSpecials)
        {
            if (currentSpecials < 0 || currentSpecials > player.playerSo.maxValue)
                return;
         
            ClearFiller(specials);

            for (var i = 0; i < currentSpecials; i++)
            {
                var prefab = Instantiate(iconSpecialFull);
                prefab.transform.SetParent(specials.transform);
                SetNormalScale(prefab);
            }
            
            for (var i = 0; i < player.playerSo.maxValue - currentSpecials; i++)
            {
                var prefab = Instantiate(iconSpecialEmpty);
                prefab.transform.SetParent(specials.transform);
                SetNormalScale(prefab);
            }
        }

        private static void SetNormalScale(GameObject prefab)
        {
            prefab.GetComponent<RectTransform>().localScale = new Vector3(0.48757f, 0.48757f, 0.48757f);
        }
    }
}
