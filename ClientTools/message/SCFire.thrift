include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct SCFire
{
	1: i32 playerUid
	10:list<common.FireInfo> fireInfoList
}