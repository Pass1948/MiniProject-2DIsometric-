using MemoryPack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MemoryPackable]
[Serializable]
public partial class SaveData
{
    /// <summary>
    /// ID
    /// </summary>
    /// <value></value>
    [MemoryPackOrder(0)]
    public int Id { get; protected set; } = default;

    /// <summary>
    /// ���� �Ͻý�
    /// </summary>
    /// <value></value>
    [MemoryPackOrder(1)]
    public string Message { get; protected set; } = null;

    /// <summary>
    /// �� ������ �÷���
    /// </summary>
    [MemoryPackIgnore]
    public bool IsEmpty => this.Id == default;

    public SaveData()
    {
    }

    [MemoryPackConstructor]
    public SaveData(int id, string message)
    {
        this.Id = id;
        this.Message = message;
    }
}
