include "struct.thrift"
namespace csharp Config.Table


struct LimitConfgTable {
	1: map<i32, struct.LimitGroup> limitMap
}

struct FuncConfigTable {
	1: map<i32, struct.FuncGroup> funcMap
}

struct TargetConfigTable {
	1: map<i32, struct.TargetGroup> targetMap
}
struct StateConflictTable{
	1:map<i32,struct.StateConflictConfig> stateConflictConfigMap
}
struct CharactorConfigTable
{
	1:map<i32,struct.CharactorConfig> charactorCofigMap
}
struct NpcConfigTable
{
	1:map<i32,struct.NpcConfig> npcCofigMap
}
struct MessageConfigTable
{
	1:map<string,map<i32,string>> messageMap
}
struct DialogConfigTable
{
	1:map<i32,struct.DialogConfig> dialogConfigMap
}
struct MainMissionConfigTable
{
	1:map<i32,struct.MainMissionConfig> mainMissionConfigMap
}
struct StageConfigTable 
{
	1: map<i32, struct.StageConfig> stageConfigMap
}
struct MissionStepConfigTable 
{
	1: map<i32, list<struct.MissionStepConfig>> missionStepByMissionIdConfigMap
	2: map<i32, struct.MissionStepConfig> missionStepByStepIdConfigMap
}
struct SkillConfigTable
{
	1: map<i32,struct.SkillConfig> skillConfigMap
}
struct RatioGameConfigTable
{
	1: list<struct.MiniGameHardConfig> ballCount
	2: list<struct.MiniGameHardConfig> ballColor
	3: list<struct.MiniGameHardConfig> ballMaterial
	4: list<struct.MiniGameHardConfig> ballSpeed
}
struct ItemConfigTable 
{
	1: map<i32, struct.ItemConfig> itemConfigMap
}
struct AIConfigTable
{
	1: string btTreeXml
}
struct ArithmeticConfigTable
{
	10:list<struct.ArithmeticTimer> timerList
	20:list<struct.ArithmeticQuestion> questionList
}
struct RegularityGameConfigTable
{
	1:  list<struct.RegularityGameConfig> regularityConfigMap
}
struct RegularityGameSettingTable
{
	1: i32 playTime
	2: i32 playCountLimit
}
struct FlightGameConfigTable
{
	1:list<struct.FlightGameConfig> flightConfigList;
}
struct MusicGameConfigTable
{
	10:  list<struct.MusicGameRangeConfig> musicRangeConfigMap
	20:  list<struct.MusicGameSpeedConfig> musicSpeedConfigMap
	30:  list<struct.MusicGameErrorConfig> musicErrorConfigMap
}
struct MusicGameSettingTable
{
	1:  map<i32,struct.MusicGameNoteKeyConfig> musicConfigMap
}
struct RunnerGameSettingTable
{
	1: double initSpeed;
	2: double gravity;
	3: double jumpSpeed;
	4: double jumpStartRiseTime;
	5: double jumpGlideTime;
	6: double superJumpSpeed;
	7: double superJumpStartRiseTime;
	8: double superJumpGlideTime; 
	9: double jumpEndDelayTime;
	10: i32 trunkLoopCount;
	11: list<double> hitWaitTimeList;
}
struct LimitFuncSceneConfigTable 
{
	/** <key: sceneId,   value: List<LimitFuncSceneConfig>> */
	1: map<i32, list<struct.LimitFuncSceneConfig>> limitFuncSceneConfigMap
}
struct TrunkConfigTable
{
	1: string trunkConfigXml
}
struct StoryConfigTable
{
	1: map<i32,list<struct.StoryConfig>> storyConfigMap
}
struct GrenadeConfigTable
{
	1:list<struct.GrenadeConfig> grenadeConfig
}