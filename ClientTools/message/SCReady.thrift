include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct SCReady
{
	10:bool isSucceed
	20:string errorInfo
}