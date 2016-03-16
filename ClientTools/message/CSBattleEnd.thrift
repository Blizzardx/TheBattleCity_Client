include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSBattleEnd
{
	10:i32 playerUid
	20:bool isWin
}