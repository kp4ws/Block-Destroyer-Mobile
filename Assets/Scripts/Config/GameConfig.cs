///*
//* Copyright (c) Kp4ws
//*
//*/

using BDM.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace BDM.Config
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Game Configuration", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public bool autoPlay = false;
        public bool isMobile = true;

        [Header("Game difficulties")]
        public float easySpeed;
        public float normalSpeed;
        public float hardSpeed;
        public float intenseSpeed;

        [Header("Map Dimensions")]
        public int minX = 0;
        public int maxX = 6;
        public int minY = 5;
        public int maxY = 14;

        public float gameGravity;

        private void OnEnable()
        {
            Physics2D.gravity = new Vector2(0, gameGravity);
        }
    }
}