using System;
using System.Resources;
using System.Xml.Linq;
using Assets.Scripts.Core.Utils;
using Communication;
using Config;
using Config.Table;
using Thrift.Protocol;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Thrift.Transport;
using System.Text;

public class ConfigManager : Singleton<ConfigManager>
{
    #region system function
    private Dictionary<string, TBase> m_ConfigPool = new Dictionary<string, TBase>();

    public T TryGetConfig<T>(string path) where T : class, TBase, new()
    {
        TBase config = null;
        if (m_ConfigPool.TryGetValue(path, out config))
        {
            return config as T;
        }
        T configInstance = default(T);
        if (ResourceManager.DecodePersonalDataTemplate<T>(GetConfigPath() + path, ref configInstance))
        {
            m_ConfigPool.Add(path, configInstance);
        }
        return configInstance;
    }
    public void TrySetConfig(string path, TBase value)
    {
        if (m_ConfigPool.ContainsKey(path))
        {
            m_ConfigPool[path] = value;
        }
        else
        {
            m_ConfigPool.Add(path, value);
        }
        //save to file
        FileUtils.EnsureFolder(GetConfigPath() + path);
        byte[] buffer = ThriftSerialize.Serialize(value);
        FileUtils.WriteByteFile(GetConfigPath() + path, buffer);
    }
    #endregion

    #region config path define

    public const string Config_LangageDefine = "zh_CN";
    public const string ConfigPath_VersionConfig = "version_txtpkg.bytes";
    public const string ConfigPath_StateConflictConfig = "";
    public const string ConfigPath_FunctionGroupConfig = "config/funcConfig_txtpkg.bytes";
    public const string ConfigPath_LimitGroupConfig = "config/limitConfig_txtpkg.bytes";
    public const string ConfigPath_TargetGroupConfig = "config/targetConfig_txtpkg.bytes";
    public const string ConfigPath_MessageConfig = "config/messageConfig_txtpkg.bytes";
    public const string ConfigPath_TerrainConfig = "config/terrainConfig_txtpkg.bytes";
    public const string ConfigPath_CharactorConfig = "config/charactorConfig_txtpkg.bytes";
    public const string ConfigPath_NpcConfig = "config/npcConfig_txtpkg.bytes";
    public const string ConfigPath_DialogConfig = "config/dialogConfig_txtpkg.bytes";
    public const string ConfigPath_MissionStepConfig = "config/missionStepConfig_txtpkg.bytes";
    public const string ConfigPath_StageConfig = "config/stageConfig_txtpkg.bytes";
    public const string ConfigPath_MainMissionConfig = "config/mainMissionConfig_txtpkg.bytes";
    public const string ConfigPath_SkillConfig = "config/skillConfig_txtpkg.bytes";
    public const string ConfigPath_RatioGameConfig = "config/ratioGameConfig_txtpkg.bytes";
    public const string ConfigPath_ItemConfig = "config/itemConfig_txtpkg.bytes";
    public const string ConfigPath_AIConfig = "config/aiConfig_txtpkg.bytes";
    public const string ConfigPath_ArithmeticConfig = "config/arithmeticConfig_txtpkg.bytes";
    public const string ConfigPath_flightConfig = "config/flightConfig_txtpkg.bytes";
    public const string ConfigPath_RegularityConfig = "config/regularityConfig_txtpkg.bytes";
    public const string ConfigPath_RegularitySettingConfig = "config/regularitySettingConfig_txtpkg.bytes";
    public const string ConfigPath_ActionConfig = "config/actionConfig_txtpkg.bytes";
    public const string ConfigPath_DifficultyConfig = "config/difficultyConfig_txtpkg.bytes";
    public const string ConfigPath_TalentConfig = "config/talentConfig_txtpkg.bytes";
    public const string ConfigPath_DefaultUserTalentConfig = "config/defaultTalentConfig_txtpkg.bytes";
    public const string ConfigPath_MusicGameConfig = "config/musicGameConfig_txtpkg.bytes";
    public const string ConfigPath_MusicGameSettingConfig = "config/musicGameSettingConfig_txtpkg.bytes";
    public const string ConfigPath_RunnerGameSettingConfig = "config/runnerGameSettingConfig_txtpkg.bytes";
    public const string ConfigPath_LimitFuncSceneConfig = "config/limitFuncSceneConfig_txtpkg.bytes";
    public const string ConfigPath_RunnerTrunkConfig = "config/runnerTrunkConfig_txtpkg.bytes";
    public const string ConfigPath_StoryConfig = "config/storyConfig_txtpkg.bytes";
    public const string ConfigPath_GrenadeConfig = "config/grenadeConfig_txtpkg.bytes";
    #endregion

    #region config handler
    public void SaveLocalVersionConfig(VersionConfig versionConfig)
    {
        TrySetConfig(ConfigPath_VersionConfig, versionConfig);
    }
    public string GetConfigPath()
    {
        return Application.persistentDataPath + "/Download/";
    }
    public VersionConfig GetLocalVersionConfig()
    {
        VersionConfig config = TryGetConfig<VersionConfig>(ConfigPath_VersionConfig);
        return config;
    }
    public StateConflictConfig GetStateConflicMap(int uid)
    {
        //get table
        StateConflictTable config =
            TryGetConfig<StateConflictTable>(ConfigPath_StateConflictConfig);

        if (null != config)
        {
            //match uid to sate conflict table
            StateConflictConfig res = null;
            if (config.StateConflictConfigMap.TryGetValue(uid, out res))
            {
                return res;
            }
        }
        return null;
    }
    public FuncGroup GetFuncGroup(int iFuncGroupId)
    {
        FuncConfigTable config = TryGetConfig<FuncConfigTable>(ConfigPath_FunctionGroupConfig);
        FuncGroup result = null;
        config.FuncMap.TryGetValue(iFuncGroupId, out result);
        return result;
    }
    public LimitGroup GetLimitGroup(int iLimitGroupId)
    {
        LimitConfgTable config = TryGetConfig<LimitConfgTable>(ConfigPath_LimitGroupConfig);
        LimitGroup result = null;
        config.LimitMap.TryGetValue(iLimitGroupId, out result);
        return result;
    }
    public TargetGroup GetTargetGroup(int iTargetGroupId)
    {
        TargetConfigTable config = TryGetConfig<TargetConfigTable>(ConfigPath_TargetGroupConfig);
        TargetGroup result = null;
        config.TargetMap.TryGetValue(iTargetGroupId, out result);
        return result;
    }
    public ArithmeticConfigTable GetArithmeticConfigTable()
    {
        ArithmeticConfigTable config = TryGetConfig<ArithmeticConfigTable>(ConfigPath_ArithmeticConfig);
        return config;
    }
    public FlightGameConfigTable GetFlightGameConfigTable()
    {
        FlightGameConfigTable config = TryGetConfig<FlightGameConfigTable>(ConfigPath_flightConfig);
        return config;
    }
    public CharactorConfigTable GetCharactorConfigTable()
    {
        CharactorConfigTable config = TryGetConfig<CharactorConfigTable>(ConfigPath_CharactorConfig);
        return config;
    }
    public CharactorConfig GetCharactorConfig(int id)
    {
        CharactorConfigTable config = TryGetConfig<CharactorConfigTable>(ConfigPath_CharactorConfig);
        CharactorConfig res = null;
        config.CharactorCofigMap.TryGetValue(id, out res);
        return res;
    }
    public NpcConfig GetNpcConfig(int id)
    {
        NpcConfigTable config = TryGetConfig<NpcConfigTable>(ConfigPath_NpcConfig);
        NpcConfig res = null;
        config.NpcCofigMap.TryGetValue(id, out res);
        return res;
    }
    public NpcConfigTable GetNpcTable()
    {
        NpcConfigTable config = TryGetConfig<NpcConfigTable>(ConfigPath_NpcConfig);
        return config;
    }
    public RatioGameConfigTable GetRatioGameConfig()
    {
        RatioGameConfigTable config = TryGetConfig<RatioGameConfigTable>(ConfigPath_RatioGameConfig);
        return config;
    }
    public string GetMessage(int id)
    {
        MessageConfigTable config = TryGetConfig<MessageConfigTable>(ConfigPath_MessageConfig);
        Dictionary<int, string> resMap = null;
        string res = null;
        if (config.MessageMap.TryGetValue(Config_LangageDefine, out resMap))
        {
            resMap.TryGetValue(id, out res);
        }
        return res;
    }
    public DialogConfig GetDialogConfig(int id)
    {
        DialogConfigTable config = TryGetConfig<DialogConfigTable>(ConfigPath_DialogConfig);
        DialogConfig res = null;
        if (!config.DialogConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find target dialog config ,check id " + id);
        }
        return res;
    }
    public MainMissionConfig GetMainMissionConfig(int id)
    {
        MainMissionConfigTable config = TryGetConfig<MainMissionConfigTable>(ConfigPath_MainMissionConfig);
        MainMissionConfig res = null;
        if (!config.MainMissionConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find target main mission config " + id);
        }
        return res;
    }
    public List<MissionStepConfig> GetMissionStepConfigByMissioinId(int id)
    {
        MissionStepConfigTable config = TryGetConfig<MissionStepConfigTable>(ConfigPath_MissionStepConfig);
        List<MissionStepConfig> res = null;
        if (!config.MissionStepByMissionIdConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find target  mission step config " + id);
        }
        return res;
    }
    public MissionStepConfig GetMissionStepConfigByStepId(int id)
    {
        MissionStepConfigTable config = TryGetConfig<MissionStepConfigTable>(ConfigPath_MissionStepConfig);
        MissionStepConfig res = null;
        if (!config.MissionStepByStepIdConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find target  mission step config " + id);
        }
        return res;
    }
    public StageConfig GetStageConfig(int id)
    {
        StageConfigTable config = TryGetConfig<StageConfigTable>(ConfigPath_StageConfig);
        StageConfig res = null;
        if (!config.StageConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find target main mission config " + id);
        }
        return res;
    }
    public SkillConfigTable GetSkillConfigTable()
    {
        SkillConfigTable res = TryGetConfig<SkillConfigTable>(ConfigPath_SkillConfig);
        return res;
    }
    public SkillConfig GetSkillConfig(int id)
    {
        SkillConfigTable config = TryGetConfig<SkillConfigTable>(ConfigPath_SkillConfig);
        SkillConfig res = null;
        if (!config.SkillConfigMap.TryGetValue(id, out res))
        {
            Debuger.LogWarning("can't find skill config " + id);
        }
        return res;
    }
    public ItemConfig GetItemConfig(int id)
    {
        ItemConfigTable config = TryGetConfig<ItemConfigTable>(ConfigPath_ItemConfig);
        if (null == config || config.ItemConfigMap == null)
        {
            Debuger.LogWarning("Can't load item config");
        }
        ItemConfig res = null;
        config.ItemConfigMap.TryGetValue(id, out res);
        return res;
    }
    public XElement GetAIConfigTable()
    {
        AIConfigTable config = TryGetConfig<AIConfigTable>(ConfigPath_AIConfig);

        return XElement.Parse(config.BtTreeXml);
    }
    public RegularityGameConfigTable GetRegularityGameConfig()
    {
        RegularityGameConfigTable config = TryGetConfig<RegularityGameConfigTable>(ConfigPath_RegularityConfig);

        return config;
    }
    public RegularityGameSettingTable GetRegularityGameSetting()
    {
        RegularityGameSettingTable config = TryGetConfig<RegularityGameSettingTable>(ConfigPath_RegularitySettingConfig);
        return config;
    }
    public MusicGameConfigTable GetMusicGameConfig()
    {
        MusicGameConfigTable config = TryGetConfig<MusicGameConfigTable>(ConfigPath_MusicGameConfig);
        return config;
    }
    public MusicGameSettingTable GetMusicGameSettingTable()
    {
        MusicGameSettingTable config = TryGetConfig<MusicGameSettingTable>(ConfigPath_MusicGameSettingConfig);
        return config;
    }
    public RunnerGameSettingTable GetRunnerGameSettingConfig()
    {
        RunnerGameSettingTable config = TryGetConfig<RunnerGameSettingTable>(ConfigPath_RunnerGameSettingConfig);
        return config;
    }
    public LimitFuncSceneConfigTable GetLimitFuncSceneConfig()
    {
        LimitFuncSceneConfigTable config = TryGetConfig<LimitFuncSceneConfigTable>(ConfigPath_LimitFuncSceneConfig);
        return config;
    }
    public StoryConfigTable GetStoryConfigTable()
    {
        StoryConfigTable config = TryGetConfig<StoryConfigTable>(ConfigPath_StoryConfig);
        return config;
    }
    public List<StoryConfig> GetStoryConfig(int id)
    {
        //return working copy
        StoryConfigTable config = TryGetConfig<StoryConfigTable>(ConfigPath_StoryConfig);
        if (null == config || config.StoryConfigMap == null || config.StoryConfigMap.Count == 0)
        {
            return null;
        }
        List<StoryConfig> res = null;

        if(config.StoryConfigMap.TryGetValue(id, out res))
        {
            return new List<StoryConfig>(res);
        }

        return null;
    }
    public GrenadeConfigTable GetGrenadeConfigTable()
    {
        GrenadeConfigTable config = TryGetConfig<GrenadeConfigTable>(ConfigPath_GrenadeConfig);
        return config;
    }
    #endregion

    #region json to thrift test code
    private void Test()
    {
        NpcConfigTable a = TryGetConfig<NpcConfigTable>(ConfigManager.ConfigPath_NpcConfig);
        byte[] test = Serialize(a);

        Encoding encoding = Encoding.ASCII;
        string realjson = encoding.GetString(test);
        FileUtils.WriteStringFile(Application.persistentDataPath + "/test.txt", realjson);

        byte[] testcopy = encoding.GetBytes(realjson);

        NpcConfigTable b = new NpcConfigTable();
        DeSerialize(b, testcopy);

        int c = 0;
    }
    public static byte[] Serialize(TBase tbase)
    {
        if (tbase == null)
        {
            return null;
        }
        using (Stream outputStream = new MemoryStream(64))
        {
            TStreamTransport transport = new TStreamTransport(null, outputStream);
            TJSONProtocol protocol = new TJSONProtocol(transport);
            tbase.Write(protocol);
            byte[] bytes = new byte[outputStream.Length];
            outputStream.Position = 0;
            outputStream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
    }

    public static void DeSerialize(TBase tbase, byte[] bytes)
    {
        if (tbase == null || bytes == null)
        {
            return;
        }
        using (Stream inputStream = new MemoryStream(64))
        {
            inputStream.Write(bytes, 0, bytes.Length);
            inputStream.Position = 0;
            TStreamTransport transport = new TStreamTransport(inputStream, null);
            TProtocol protocol = new TJSONProtocol(transport);
            tbase.Read(protocol);
        }
    }
    #endregion
}
