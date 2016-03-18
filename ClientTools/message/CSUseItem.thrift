include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSUseItem
{
	10: i32 playerUid
	20: i32 positionId
	30: i32 itemId
}