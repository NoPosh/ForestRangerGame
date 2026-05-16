using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] private GameObject _picturePanel;

    private bool _isClosed = false;

    public void ClosePicture()
    {
        _isClosed = true;
    }

    public void ShowPhotoOnScreen()
    {
        _picturePanel.SetActive(true);
    }

    public async UniTask WaitForPhotoClosedAsync(System.Threading.CancellationToken token)
    {
        try
        {
            await UniTask.WaitUntil(() => _isClosed, cancellationToken: token);
        }
        catch (OperationCanceledException)
        {

        }
        catch (Exception e)
        {

        }
    }
}

