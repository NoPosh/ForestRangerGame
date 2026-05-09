using System;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class LevelServiceLocator
{
    //private Dictionary<Type, object> _globalServices = new();
    private Dictionary<Type, object> _services = new();

    public void Register<TService>(TService obj) where TService : class
    {
        var type = typeof(TService);

        if (_services.ContainsKey(type))
        {
            Debug.Log($"[ServiceLocator] Сервис {typeof(TService)} уже был зарегестрирован, меняем");
        }

        _services[typeof(TService)] = obj;
    }

    public TService GetService<TService>() where TService : class
    {
        var type = typeof(TService);

        if (_services.TryGetValue(type, out var service))
            return service as TService;

        Debug.Log($"Сервис {typeof(TService)} не зарегестрирован");
        return null;
    }
    public void Unregister<TService>() where TService : class
    {
        var type = typeof(TService);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}
