namespace java server.msg.auto
namespace csharp NetWork.Auto

struct ThriftVector3 
{
	1: i32 x
	2: i32 y
	3: i32 z
}
struct RoomInfo
{
	10: string name
	20: string mapName
	30: i32 roomMemberCount
}
struct PlayerInfo
{
	10: i32 uid
	20: string name
	30: string meshName
	40: i32 positionId
	50: i32 hp
}
struct ItemGenFundamental
{
	10: list<i32> positionId
	20: list<i32> itemList
	30: i32 maxCount
	40: i32 genPreTimeItemCountMin
	50: i32 genPreTimeItemCountMax
	60: i32 triggerDeltaTime
	70: i32 initItemCount
}