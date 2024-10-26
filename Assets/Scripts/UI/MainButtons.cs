using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    AudioData audioData1, audioData2;

    private void Awake()
    {
        audioData1 = AudioManager.Instance.getAudioData(AudioName.关闭菜单商店);
        audioData2 = AudioManager.Instance.getAudioData(AudioName.打开菜单商店);
    }

    public void BackGame()
    {
        AudioManager.Instance.playVFX(audioData1);
        this.gameObject.SetActive(false);
    }

    public void BackStartScene()
    {
        AudioManager.Instance.playVFX(audioData2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReStartGame()
    {
        AudioManager.Instance.playVFX(audioData2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDisable()
    {
        PlayerController.GetInstance().canInput = true;
        Time.timeScale = 1;
    }
}
