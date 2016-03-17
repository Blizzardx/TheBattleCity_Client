include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct SCUsedItem
{
	10: i32 playerUid
	20: i32 itemId
	32: i32 positionId
}