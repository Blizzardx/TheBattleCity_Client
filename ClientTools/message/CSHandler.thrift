include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSHandler
{
	10:common.ThriftVector3 position
	20:common.ThriftVector3 direction
}