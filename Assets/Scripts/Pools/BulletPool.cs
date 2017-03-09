using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : BasicPool {
    public static BulletPool instance;

    void Awake()
    {
        instance = this;
    }
}
