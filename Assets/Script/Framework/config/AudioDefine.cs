using UnityEngine;
using System;
using System.Collections;

    //background audio define
    public class BackgroundAudioDefine
    {
        #region Singleton
        private static BackgroundAudioDefine m_Instance;
        private BackgroundAudioDefine()
        {
        }
        public static BackgroundAudioDefine Singleton
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new BackgroundAudioDefine();
                }
                return m_Instance;
            }
        }
        #endregion

        public enum AudioBackgroundSoundType
        {
            LogIn,
            Menu,
            RoomList,
            WaitForBattle,
            Battle_0,
            Battle_1,
            Battle_2,
            Battle_3,
            Battle_4,
            BattleVictory,
            BattleFailed,
            Normal
        }
        //begin with 100 01 to 100 99
        public string LogIn         = "10001";
        public string Menu          = "10002";
        public string RoomList      = "10003";
        public string WaitForBattle = "10004";
        public string Battle_0      = "10005";
        public string Battle_1      = "10006";
        public string Battle_2      = "10007";
        public string Battle_3      = "10008";
        public string Battle_4      = "10012";
        public string BattleVictory = "10009";
        public string BattleFailed  = "10010";
        public string Normal        = "10011";
    }

    //efect audio define
    public class EffectAudioDefine
    {
        #region Singleton
        private static EffectAudioDefine m_Instance;
        private EffectAudioDefine()
        {
        }
        public static EffectAudioDefine Singleton
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new EffectAudioDefine();
                }
                return m_Instance;
            }
        }
        #endregion

        public enum AudioEffectSoundType
        {
            Fire,
            Explosion,
            Lightning,
            Pulse,
            Ray,
            Reload,
        }
        //begin with 101 01 to 101 99
        public string Fire          = "10101";
        public string Explosion     = "10102";
        public string Lightning     = "10103";
        public string Pulse         = "10104";
        public string Ray           = "10105";
        public string Reload        = "10106";

    }

    //sound audio define
    public class UISoundAudioDefine
    {
        #region Singleton
        private static UISoundAudioDefine m_Instance;
        private UISoundAudioDefine()
        {
        }
        public static UISoundAudioDefine Singleton
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new UISoundAudioDefine();
                }
                return m_Instance;
            }
        }
        #endregion

        public enum AudioUISoundType
        {
            OnClick,
        }

        //begin with 102 01 to 102 99
        public string OnClick = "10201";
    }
