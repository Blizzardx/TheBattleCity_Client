include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct BattleCommandData
{
	10: i32 type
	20: list<i32> argvs
}
struct BattleCharCommand
{
	10: i32 charId
	20: list<BattleCommandData> commandList
}
struct BattleFrameData
{
	10: i32 frameIndex
	20: list<BattleCharCommand> charCommandList
}
struct SCBattleLogicFrame
{
	10:list<BattleFrameData> battleFrameDataList
}
struct CSBattleLogicFrame
{
	10: BattleCommandData commandData
}