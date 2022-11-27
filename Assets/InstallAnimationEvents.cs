using UnityEngine;

public class InstallAnimationEvents : AnimationEvents
{
    [SerializeField] private GameObject InstalledButton;
    
    public void AnimEvent_ActivateOtherObjectAndCommitDie()
    {
        print(InstalledButton.activeInHierarchy);
        InstalledButton.SetActive(true);
        print(InstalledButton.activeInHierarchy);
        Destroy(gameObject);
    }
}
