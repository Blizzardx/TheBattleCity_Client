include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSHandler
{
	1: i32 playerUid
	10:common.ThriftVector3 currentPosition
	20:common.ThriftVector3 moveDirection
}