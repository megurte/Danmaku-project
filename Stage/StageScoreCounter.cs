using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boss;
using Character;
using TMPro;
using UI.Scene.Additional;
using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StageScoreCounter: MonoBehaviour
    {
        [Header("TMP references")]
        [SerializeField] private TextMeshProUGUI stageClearedTextUI;
        [SerializeField] private TextMeshProUGUI stagePointsTextUI;
        [SerializeField] private TextMeshProUGUI finishButtonTextUI;
        
        [Header("Points Settings")]
        [SerializeField] private int stageNumber;
        [SerializeField] private int stagePoints;
        [SerializeField] private int noSpecialUsePoints;
        [SerializeField] private int noDamageTaken;
        [SerializeField] private List<int> difficultyPoints;

        [Header("Other references")]
        [SerializeField] private GameObject additionalPointsUIPrefab;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button finishButton;

        private int _cumulativeValue = default;
        private int _collectedPoints = default;
        private bool _specialUsed = default;
        private int _currentRunDifficultyPoints = default;

        private void Start()
        {
            BossTimer.TranslateTimerData.AddListener(SetScoreForTimeRemaining);
            PlayerBase.TranslateCurrentStageScore.AddListener((int points) => _collectedPoints = points);
            PlayerBase.SpecialUsed.AddListener( (int x) => _specialUsed = true);
        }

        private void SetScoreForTimeRemaining(Dictionary<int, int> phasesTime)
        {
            stageClearedTextUI.text = $"Stage {stageNumber} Cleared";
            stagePointsTextUI.text = $"{stagePoints}";

            stageClearedTextUI.gameObject.SetActive(true);
            stagePointsTextUI.gameObject.SetActive(true);
            StartCoroutine(SpawnAndCumulatePoints(phasesTime));
        }

        private IEnumerator SpawnAndCumulatePoints(Dictionary<int, int> phasesTime)
        {
            var additionalPointsList = new List<GameObject>();
            
            _cumulativeValue = stagePoints;

            yield return new WaitForSeconds(2);

            foreach (var item in phasesTime.Where(item => item.Key > 0))
            {
                var placeholder = CreatePointsPlaceholder($"+{item.Value}: {item.Key} sec");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
                
                StartCoroutine(CumulatePoints(phasesTime[item.Key]));
            }

            if (!_specialUsed)
            {
                var placeholder = CreatePointsPlaceholder($"+{noSpecialUsePoints}: no special");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
                
                StartCoroutine(CumulatePoints(noDamageTaken));
            }

            if (PlayerBase.NoDamage)
            {
                var placeholder = CreatePointsPlaceholder($"+{noDamageTaken}: no damage taken");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
                
                StartCoroutine(CumulatePoints(noSpecialUsePoints));
            }

            _currentRunDifficultyPoints = PlayerRunInfo.GetRunDifficulty() switch
            {
                Difficulty.Easy => difficultyPoints[(int)Difficulty.Easy],
                Difficulty.Normal => difficultyPoints[(int)Difficulty.Normal],
                Difficulty.Hellfire => difficultyPoints[(int)Difficulty.Hellfire],
                _ => _currentRunDifficultyPoints
            };

            if (_currentRunDifficultyPoints > 0)
            {
                var placeholder = CreatePointsPlaceholder($"+{_currentRunDifficultyPoints}: " 
                                                          + $"{PlayerRunInfo.GetRunDifficulty().ToString()}");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
                
                StartCoroutine(CumulatePoints(_currentRunDifficultyPoints));
            }

            if (_collectedPoints > 0)
            {
                var placeholder = CreatePointsPlaceholder($"+{_collectedPoints}: collected points");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
                
                StartCoroutine(CumulatePoints(_collectedPoints));
            }
            
            PlayerRunInfo.AddRunScore(_cumulativeValue);
            PlayerRunInfo.SaveScoreData();

            finishButton.interactable = true;
            finishButtonTextUI.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            foreach (var item in additionalPointsList)
            {
                Destroy(item);
            }
            
            yield return new WaitForSeconds(8);

            SceneTransition.AsyncSceneLoading("MainMenu");
        }

        private IEnumerator CumulatePoints(int pointsToCumulate)
        {
            yield return new WaitForSeconds(1);
            
            _cumulativeValue += pointsToCumulate;
            stagePointsTextUI.text = $"{_cumulativeValue}";
        }

        private GameObject CreatePointsPlaceholder(string contentText)
        {
            var newPrefab = Instantiate(additionalPointsUIPrefab, 
                additionalPointsUIPrefab.transform.position, Quaternion.identity);
            newPrefab.transform.SetParent(canvas.transform, false);
            newPrefab.gameObject.GetComponent<TextMeshProUGUI>().text = contentText;
            
            return newPrefab;
        }
    }
}