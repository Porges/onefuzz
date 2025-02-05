﻿using Azure.Storage;
using Azure.Storage.Queues;
using Microsoft.OneFuzz.Service.OneFuzzLib.Orm;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.OneFuzz.Service;
public interface IQueue
{
    Task SendMessage(string name, byte[] message, StorageType storageType, TimeSpan? visibilityTimeout = null, TimeSpan? timeToLive = null);
    Task<bool> QueueObject<T>(string name, T obj, StorageType storageType, TimeSpan? visibilityTimeout);
}


public class Queue : IQueue
{
    IStorage _storage;
    ILog _logger;

    public Queue(IStorage storage, ILog logger)
    {
        _storage = storage;
        _logger = logger;
    }


    public async Task SendMessage(string name, byte[] message, StorageType storageType, TimeSpan? visibilityTimeout = null, TimeSpan? timeToLive = null)
    {
        var queue = GetQueue(name, storageType);
        if (queue != null)
        {
            try
            {
                await queue.SendMessageAsync(Convert.ToBase64String(message), visibilityTimeout: visibilityTimeout, timeToLive: timeToLive);
            }
            catch (Exception)
            {

            }
        }
    }

    public QueueClient? GetQueue(string name, StorageType storageType)
    {
        var client = GetQueueClient(storageType);
        try
        {
            return client.GetQueueClient(name);
        }
        catch (Exception)
        {
            return null;
        }
    }


    public QueueServiceClient GetQueueClient(StorageType storageType)
    {
        var accountId = _storage.GetPrimaryAccount(storageType);
        //_logger.LogDEbug("getting blob container (account_id: %s)", account_id)
        (var name, var key) = _storage.GetStorageAccountNameAndKey(accountId);
        var accountUrl = new Uri($"https://%s.queue.core.windows.net{name}");
        var client = new QueueServiceClient(accountUrl, new StorageSharedKeyCredential(name, key));
        return client;
    }

    public async Task<bool> QueueObject<T>(string name, T obj, StorageType storageType, TimeSpan? visibilityTimeout)
    {
        var queue = GetQueue(name, storageType) ?? throw new Exception($"unable to queue object, no such queue: {name}");

        var serialized = JsonSerializer.Serialize(obj, EntityConverter.GetJsonSerializerOptions());
        //var encoded = Encoding.UTF8.GetBytes(serialized);

        try
        {
            await queue.SendMessageAsync(serialized, visibilityTimeout: visibilityTimeout);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
