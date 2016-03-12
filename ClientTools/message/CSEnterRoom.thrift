include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSEnterRoom
{
	10:string roomName
	20:string playerName
}