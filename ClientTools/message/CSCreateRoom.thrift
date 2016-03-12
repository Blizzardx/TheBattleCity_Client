include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct CSCreateRoom
{
	10:common.RoomInfo roomInformation
	20:string playerName
}