
namespace csharp Config

typedef i32 ItemId
typedef i32 GroupId
typedef i32 LimitId
typedef i32 LimitGroupId
typedef i32 FuncId
typedef i32 FuncGroupId


struct LimitData {
	1: LimitId id
	2: byte oper
	3: i32 messageId
	4: byte target
	5: list<string> paramStringList
	6: list<i32> paramIntList
}

struct LimitGroup {
	1: LimitGroupId id
	2: byte logic
	3: list<LimitData> limitDataList
}


struct FuncData {
	1: FuncId id
	2: byte oper
	3: byte target
	4: list<string> paramStringList
	5: list<i32> paramIntList
}

struct FuncGroup {
	1: FuncGroupId id
	2: list<FuncData> funcDataList
}

struct TargetData {
	1: i32 targetId
	2: list<string> paramStringList
	3: list<i32> paramIntList
}

struct TargetGroup {
	1: i32 id
	2: list<TargetData> targetDataList
}
struct StateConflictConfigElement {
	10: i32 stateId
	20: bool isConflict
}
struct StateConflictConfig {
	10: map<i32,list<StateConflictConfigElement>> stateConflictMap
}
struct CharactorConfig
{
	1: i32 id
	2: i32 nameMsgId
	3: i32 sex
	4: string ModelResource
	5: i32 age
}
struct NpcConfig
{
	1: i32 id
	2: i32 nameMsgId
	3: i32 sex
	4: string ModelResource
	5: i32 age
	6: i32 aiId
	7: i32 clickFuncId
	8: bool isInGroup
}
struct DialogConfig
{
	1: i32 id
	2: i32 messageId
	3: string audioId
}
struct MainMissionConfig
{
  10: i32 id
  20: i32 nameMessageId
  30: string nameAudioId
  40: i32 desMessageId
  50: string desAudioId
  60: i32 acceptLimitId
  70: i32 completeLimitId
  80: i32 completeFuncId
  90: i32 nextMissionId
}
struct StageConfig 
{
	10: i32 id
	11: i32 nextStageId
	20: i32 nameMessageId
	21: string nameAudioId
	30: i32 descMessageId
	31: string descAudioId
	40: i32 helpMessageId
	41: string helpAudioId
	50: i32 stageMapId
	51: i16 recommendLevel
	60: string stageIcon
	90: i32 enterLimitId
	100: i32 enterFuncId
	130: i32 winLimitId
	140: i32 winFuncId
	141: i32 loseFuncId
}
struct MissionStepConfig 
{
	1:  i32 id
	10: i32 missionId
	20: i32 sceneId
	30: i32 sceneLimitId
	40: i32 sceneFuncId
	50: i32	completeLimitId
	60: i32	completeFuncId
	61: i32 guideMessageId
	62: string guideAudioId
}
struct SkillConfig
{
	1:  i32 id
	10: i32 nameMessageId
	20: i32 descMessageId
	30: string skillIcon
	40: i32 actionId
	50: i32 priority
	60: i32 skillType
	70: i32 quality
	80: i32 level
	81: i32 beginCd
	90: i32 initCd
	100: i32 addCd
	110: i32 perLimitId
	120: i32 perFuncId
	130: i32 targeteId
	140: i32 limitId
	150: i32 funcId
}
struct MiniGameHardConfig
{
	10:	double hard
	20:	i32 count
}
struct ItemConfig 
{
	10: i32 id
	20: i32 nameMessageId
	30: i32 descMessageId
	50: string icon
	51: string dropIcon
	60: byte quality
	70: i32 sellGold
	80: i32 useLimitId
	90: i32 useFuncId
	100:i32 accessMessageId
}
struct ArithmeticTimer
{
	10:double difficulty
	20:i32 timer
}
struct ArithmeticItem
{
	10:i32 expression
	20:optional string operation
}
struct ArithmeticQuestion
{
	10:double difficulty
	20:string optionContent
	30:list<ArithmeticItem> itemList
}
struct RegularityGameOption
{
	10: string name
	20: bool isVisable
}
struct RegularityGameConfig
{
	10: double difficultyid
	20: list<RegularityGameOption> optionList
	30: list<string> answerList
}
struct FlightGameConfig
{
	10:double difficultyid
	20:double frequency
}
struct MusicGameRangeConfig
{
	10:double difficultyid
	20:double range
}
struct MusicGameSpeedConfig
{
	10:double difficultyid
	20:double speed
}
struct MusicGameErrorConfig
{
	10:double difficultyid
	20:i32 errorCount
}
struct MusicGameNoteKey
{
	10: double 	time
	20: i32 	key
}
struct MusicGameNoteKeyConfig
{
	20: list<MusicGameNoteKey> noteKeyList
}
struct LimitFuncSceneConfig 
{
	1: i32 limitId
	2: i32 funcId
	3: i32 targetId
}
struct StoryConfig
{
	10: i32 id
	20: i32 dialogId
	30: i32 talkerId
	40: bool isFocusOnTalker
	50: i32 delay
}
struct GrenadeConfig
{
	10: i32 diff
	20: i32 respRange
	30: i32 waitTime
}