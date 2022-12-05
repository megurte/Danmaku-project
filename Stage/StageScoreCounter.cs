using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boss;
using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StageScoreCounter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stageClearedTextUI;
        [SerializeField] private TextMeshProUGUI stagePointsTextUI;

        [Header("Points Settings")]
        [SerializeField] private int stageNumber;
        [SerializeField] private int stagePoints;
        [SerializeField] private int noSpecialUsePoints;
        [SerializeField] private int noDamageTaken;
        [SerializeField] private List<int> difficultyPoints;

        [Space(20f)]
        [SerializeField] private GameObject additionalPointsUIPrefab;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button finishButton;

        private int _cumulativeValue = default;
        private bool _specialUsed = default;
        private int _currentRunDifficultyPoints = default;

        private void Start()
        {
            BossTimer.TranslateTimerData.AddListener(SetScoreForTimeRemaining);
            PlayerBase.SpecialUsed.AddListener( (int x) => _specialUsed = true);
        }

        private void SetScoreForTimeRemaining(Dictionary<int, int> phasesTime)
        {
            stageClearedTextUI.text = $"Stage {stageNumber} Cleared";
            stagePointsTextUI.text = $"{stagePoints}";

            stageClearedTextUI.gameObject.SetActive(true);
            stagePointsTextUI.gameObject.SetActive(true);
            finishButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);
            StartCoroutine(SpawnCompletePointsPlaceholders(phasesTime));
            StartCoroutine(CumulateCompletePoints(phasesTime));
            StartCoroutine(AddCompletePointsToScore());
        }

        private IEnumerator SpawnCompletePointsPlaceholders(Dictionary<int, int> phasesTime)
        {
            var additionalPointsList = new List<GameObject>();
            
            yield return new WaitForSeconds(2);

            foreach (var item in phasesTime.Where(item => item.Key > 0))
            {
                var placeholder = CreatePointsPlaceholder($"+{item.Value}: {item.Key} sec");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
            }

            if (!_specialUsed)
            {
                var placeholder = CreatePointsPlaceholder($"+{noSpecialUsePoints}: no special");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
            }

            if (PlayerBase.NoDamage)
            {
                var placeholder = CreatePointsPlaceholder($"+{noDamageTaken}: no damage taken");
                additionalPointsList.Add(placeholder);
                
                yield return new WaitForSeconds(1);
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
                                                          + $"${PlayerRunInfo.GetRunDifficulty().ToString().ToUpper()}");
                additionalPointsList.Add(placeholder);
            }
            
            yield return new WaitForSeconds(2);

            foreach (var item in additionalPointsList)
            {
                Destroy(item);
            }
        }
        
        private IEnumerator CumulateCompletePoints(Dictionary<int, int> phasesTime)
        {
            _cumulativeValue = stagePoints;
            
            yield return new WaitForSeconds(3);

            foreach (var item in phasesTime.Where(item => item.Key > 0))
            {
                yield return new WaitForSeconds(1);
                    
                _cumulativeValue += phasesTime[item.Key];
                stagePointsTextUI.text = $"{_cumulativeValue}";
            }

            if (!_specialUsed)
            {
                yield return new WaitForSeconds(1);
                
                _cumulativeValue += noDamageTaken;
                stagePointsTextUI.text = $"{_cumulativeValue}";
            }
            
            if (PlayerBase.NoDamage)
            {
                yield return new WaitForSeconds(1);
                
                _cumulativeValue += noSpecialUsePoints;
                stagePointsTextUI.text = $"{_cumulativeValue}";
            }
            
            yield return new WaitForSeconds(1);
                
            _cumulativeValue += _currentRunDifficultyPoints;
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

        private IEnumerator AddCompletePointsToScore()
        {
            yield return new WaitForSeconds(12);

            finishButton.interactable = true;
            PlayerRunInfo.AddRunScore(_cumulativeValue);
        }
    }
}