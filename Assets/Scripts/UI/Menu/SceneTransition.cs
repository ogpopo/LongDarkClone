using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text LoadingPercentage;
    [SerializeField] private Image LoadingProgressBar;
    
    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false; 
    
    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        instance.loadingSceneOperation.allowSceneActivation = false;
        
        instance.LoadingProgressBar.fillAmount = 0;
    }
    
    private void Start()
    {
        instance = this;
        
        componentAnimator = GetComponent<Animator>();
        
        if (shouldPlayOpeningAnimation) 
        {
            componentAnimator.SetTrigger("sceneOpening");
            instance.LoadingProgressBar.fillAmount = 1;

            shouldPlayOpeningAnimation = false; 
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            
            // Просто присвоить прогресс:
            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 
            
            // Присвоить прогресс с быстрой анимацией, чтобы ощущалось плавнее:
            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        
        loadingSceneOperation.allowSceneActivation = true;
    }
}