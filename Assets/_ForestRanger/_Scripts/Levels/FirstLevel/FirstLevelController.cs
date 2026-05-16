using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelController: MonoBehaviour
{
    [SerializeField] private PlaceForPhoto _photo;
    [SerializeField] private PhotoManager _photoManager;
    [SerializeField] private UI_ScreenFader _fader;
    
    private void Start()
    {
        StartLevelLogic(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTask StartLevelLogic(System.Threading.CancellationToken token)
    {
        try
        {
            // 0. Опционально: Ждем завершения анимации появления уровня
            Debug.Log("Уровень запущен");
            _fader.SetBlack();
            await _fader.FadeInAsync(2, token);

            // 1. Тут могла быть логика комикса
            // await PlayComicAsync(token);

            // 2. Ждем сигнал о том, что сделали фотку
            Debug.Log("Ждем, пока игрок сделает фото...");
            await _photo.OnPictureTaked.FirstAsync(cancellationToken: token);

            // 3. После сигнала выводим ее на экран
            Debug.Log("Фото получено. Показываем на экране.");
            _photoManager.ShowPhotoOnScreen();  //Открытие фотки

            // 4. Ждем, пока игрок закроет фотографию (например, нажмет на крестик)
            Debug.Log("Ждем закрытия фотографии игроком...");
            await _photoManager.WaitForPhotoClosedAsync(token);

            // 5. Затемнение экрана
            Debug.Log("Затемнение экрана...");
            await _fader.FadeOutAsync(1, token);

            // 6. Переключение сцены
            Debug.Log("Переключение сцены...");
            SceneManager.LoadScene("GameplayScene2");
            //await _sceneLoader.LoadNextSceneAsync();
        }
        catch (OperationCanceledException)
        {
            // Этот блок отработает, если объект уничтожили во время ожидания (например, вышли в меню)
            Debug.Log("Логика уровня была отменена, так как объект уничтожен.");
        }
        catch (Exception e)
        {
            // Ловим остальные непредвиденные ошибки
            Debug.LogError($"Ошибка в логике уровня: {e}");
        }
    }
}
