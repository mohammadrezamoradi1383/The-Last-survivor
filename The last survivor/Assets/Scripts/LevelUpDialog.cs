using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpDialog : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelIcon;
    [SerializeField] private Animator levelUpAnimator;

    public void ShowDialog(int value)
    {
       SetIcon(value);
       levelUpAnimator.SetBool("LevelUp", true);
       Invoke(nameof(HideDialog), 3f);
    }
    private void SetIcon(int value)
    {
        for (int i = 0; i < levelIcon.Count; i++)
        {
            levelIcon[i].SetActive(i <= value);
        }
    }

    private void HideDialog()
    {
        levelUpAnimator.SetBool("LevelUp", false);
    }
}
