include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct SCCreateRoom
{
	10:bool isSucceed
	20:string errorInfo
	30:i32 playerUid
}