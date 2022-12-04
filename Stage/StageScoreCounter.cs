using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boss;
using Character;
using TMPro;
using UnityEngine;

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
                var newHolder = CreateAdditionalPointsHolder();
                newHolder.gameObject.GetComponent<TextMeshProUGUI>().text = $"+{item.Value}: {item.Key} sec";
                additionalPointsList.Add(newHolder);
                
                yield return new WaitForSeconds(1);
            }

            if (!_specialUsed)
            {
                var newHolder = CreateAdditionalPointsHolder();
                newHolder.gameObject.GetComponent<TextMeshProUGUI>().text = $"+{noSpecialUsePoints}: no special";
                additionalPointsList.Add(newHolder);
                
                yield return new WaitForSeconds(1);
            }

            if (PlayerBase.NoDamage)
            {
                var newHolder = CreateAdditionalPointsHolder();
                newHolder.gameObject.GetComponent<TextMeshProUGUI>().text = $"+{noDamageTaken}: no damage taken";
                additionalPointsList.Add(newHolder);
                
                yield return new WaitForSeconds(1);
            }
            
            switch (PlayerRunInfo.GetRunDifficulty())
            {
                case Difficulty.Normal:
                    _currentRunDifficultyPoints = difficultyPoints[0];
                    break;
                case Difficulty.Hard:
                    _currentRunDifficultyPoints = difficultyPoints[1];
                    break;
                case Difficulty.HellFire:
                    _currentRunDifficultyPoints = difficultyPoints[2];
                    break;
            }

            if (_currentRunDifficultyPoints > 0)
            {
                var newHolder = CreateAdditionalPointsHolder();
                newHolder.gameObject.GetComponent<TextMeshProUGUI>().text 
                    = $"+{_currentRunDifficultyPoints}: ${PlayerRunInfo.GetRunDifficulty().ToString().ToUpper()}";
                additionalPointsList.Add(newHolder);
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

        private GameObject CreateAdditionalPointsHolder()
        {
            var newHolder = Instantiate(additionalPointsUIPrefab, 
                additionalPointsUIPrefab.transform.position, Quaternion.identity);
            newHolder.transform.SetParent(canvas.transform, false);
            
            return newHolder;
        }

        private IEnumerator AddCompletePointsToScore()
        {
            yield return new WaitForSeconds(12);

            PlayerRunInfo.AddRunScore(_cumulativeValue);
        }
    }
}