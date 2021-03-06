﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets assets;

    public static GameAssets i {
        get {
            if (assets == null) 
            {
                assets = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return assets;
        }
    }

    public GameObject scorePopupPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject teleporterPrefab;
    public GameObject triangleExplosion;
    public GameObject defaultShot;
    public GameObject dropIcon;
    public GameObject firePS;
}
