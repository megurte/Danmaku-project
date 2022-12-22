using System;
using Character;
using Drop;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Scene.Player
{
    public class UIPlayerPanel : MonoBehaviour
    {
        [SerializeField] private GameObject health;
        [SerializeField] private GameObject specials;
    
        [Header("Experience"), SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI experienceText;
        
        [Space(20f), SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Prefabs"), SerializeField] private GameObject iconHealthFull;
        [SerializeField] private GameObject iconHealthEmpty;
        [SerializeField] private GameObject iconSpecialFull;
        [SerializeField] private GameObject iconSpecialEmpty;
        
        [Space(20f), SerializeField] private GameObject deathText;
        [SerializeField] private GameObject scoreDeathText;

        private PlayerBase _player;
        private PlayerSpecials _playerSpecials;

        [Inject]
        public void Construct(PlayerBase settings)
        {
            _player = settings;
            _playerSpecials = _player.GetComponent<PlayerSpecials>();
        }
        
        private void Start()
        {
            UpdateHealthPlaceholder(_player.playerScriptableObject.health);
            UpdateSpecialPlaceholder(_playerSpecials.SpecialsCount);

            PlayerBase.OnDeath.AddListener(ShowDeathScreen);
            PlayerBase.SpecialUsed.AddListener(UpdateSpecialPlaceholder);
            PlayerBase.UpdatePlayerUI.AddListener(UpdateExperienceUI);
            GlobalEvents.OnHealthChange.AddListener(UpdateHealthPlaceholder);
            GlobalEvents.OnSpecialChange.AddListener(UpdateSpecialPlaceholder);
        }

        private void UpdateExperienceUI()
        {
            var levelUpMap = _player.playerScriptableObject.levelUpMap;
        
            levelText.text = $"Lv. {_player.Level}";;
            points.text = _player.Points + "";
            scoreText.text = _player.Points + "";

            if (_player.Level + 1 <= levelUpMap.keys.Count)
            {
                experienceText.text = $"{_player.Experience}/{levelUpMap.values[_player.Level]}";
            }
            else
            {
                experienceText.text = $"{levelUpMap.values[levelUpMap.values.Count - 1]}/" +
                                      $"{levelUpMap.values[levelUpMap.values.Count - 1]}";
            }
        }


        /// <summary>
        /// Clears the placeholder game object by removing all its children
        /// </summary>
        /// <param name="placeholder">The placeholder game object</param>
        private void ClearPlaceholder(GameObject placeholder) {
            while (placeholder.transform.childCount > 0)
            {
                var child = placeholder.transform.GetChild(0);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        
        /// <summary>
        /// Updates the filler game object for the player's health.
        /// </summary>
        /// <param name="currentHealth">The current health value.</param>
        private void UpdateHealthPlaceholder(int currentHealth)
        {
            if (currentHealth < 0 || currentHealth > _player.MaxHealth) return;
            
            ClearPlaceholder(health);
            
            var minHealth = Mathf.Min(currentHealth, _player.MaxHealth);
            
            for (var i = 0; i < minHealth; i++)
            {
                var prefab = Instantiate(iconHealthFull, health.transform, true);
                SetNormalScale(prefab);
            }

            for (var i = 0; i < _player.MaxHealth - minHealth; i++)
            {
                var prefab = Instantiate(iconHealthEmpty, health.transform, true);
                SetNormalScale(prefab);
            }
        }
        
        private void UpdateSpecialPlaceholder(int currentSpecials)
        {
            if (currentSpecials < 0 || currentSpecials > _playerSpecials.MaxSpecials)
                return;
         
            ClearPlaceholder(specials);
            
            var minSpecials = Mathf.Min(currentSpecials, _playerSpecials.MaxSpecials);
            
            for (var i = 0; i < minSpecials; i++)
            {
                var prefab = Instantiate(iconSpecialFull, specials.transform, true);
                SetNormalScale(prefab);
            }
            
            for (var i = 0; i < _playerSpecials.MaxSpecials - minSpecials; i++)
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
