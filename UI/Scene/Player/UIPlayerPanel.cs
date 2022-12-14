using Character;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI.Scene.Player
{
    public class UIPlayerPanel : MonoBehaviour
    {
        [SerializeField] private PlayerBase player;
        [SerializeField] private GameObject health;
        [SerializeField] private GameObject specials;
    
        [Header("Experience")][SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI experienceText;
        
        [Space(20f)][SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Prefabs")][SerializeField] private GameObject iconHealthFull;
        [SerializeField] private GameObject iconHealthEmpty;
        [SerializeField] private GameObject iconSpecialFull;
        [SerializeField] private GameObject iconSpecialEmpty;
        
        [Space(20f)][SerializeField] private GameObject deathText;
        [SerializeField] private GameObject scoreDeathText;

        [Inject]
        public void Construct(PlayerBase settings)
        {
            player = settings;
        }
        
        private void Start()
        {
            UpdateHealthFiller(player.playerScriptableObject.health);
            UpdateSpecialFiller(player.playerScriptableObject.special);

            PlayerBase.OnDeath.AddListener(ShowDeathScreen);
            PlayerBase.SpecialUsed.AddListener(UpdateSpecialFiller);
            GlobalEvents.OnHealthChange.AddListener(UpdateHealthFiller);
            GlobalEvents.OnSpecialChange.AddListener(UpdateSpecialFiller);
        }

        private void FixedUpdate()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            var levelUpMap = player.playerScriptableObject.levelUpMap;
        
            levelText.text = "Lv. " + player.Level;
            points.text = player.Points + "";
            scoreText.text = player.Points + "";

            if (player.Level + 1 <= levelUpMap.keys.Count)
            {
                experienceText.text = player.Experience + "/" + levelUpMap.values[player.Level];
            }
            else
            {
                experienceText.text = levelUpMap.values[levelUpMap.values.Count - 1] + "/" 
                    + levelUpMap.values[levelUpMap.values.Count - 1];
            }
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
            if (currentHealth < 0 || currentHealth > player.playerScriptableObject.maxHealth) return;
            
            ClearFiller(health);

            for (var i = 0; i < currentHealth; i++)
            {
                var prefab = Instantiate(iconHealthFull, health.transform, true);
                SetNormalScale(prefab);
            }
            
            for (var i = 0; i < player.playerScriptableObject.maxHealth - currentHealth; i++)
            {
                var prefab = Instantiate(iconHealthEmpty, health.transform, true);
                SetNormalScale(prefab);
            }
        }
        
        private void UpdateSpecialFiller(int currentSpecials)
        {
            if (currentSpecials < 0 || currentSpecials > player.playerScriptableObject.maxValue)
                return;
         
            ClearFiller(specials);

            for (var i = 0; i < currentSpecials; i++)
            {
                var prefab = Instantiate(iconSpecialFull, specials.transform, true);
                SetNormalScale(prefab);
            }
            
            for (var i = 0; i < player.playerScriptableObject.maxValue - currentSpecials; i++)
            {
                var prefab = Instantiate(iconSpecialEmpty, specials.transform, true);
                SetNormalScale(prefab);
            }
        }

        private void ShowDeathScreen(int score)
        {
            deathText.SetActive(true);
            scoreDeathText.GetComponent<TextMeshProUGUI>().text = $"Score record: {score}";
        }

        private static void SetNormalScale(GameObject prefab)
        {
            prefab.GetComponent<RectTransform>().localScale = new Vector3(0.48757f, 0.48757f, 0.48757f);
        }
    }
}
