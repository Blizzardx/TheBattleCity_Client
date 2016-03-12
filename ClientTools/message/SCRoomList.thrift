include "common.thrift"
namespace java server.msg.auto
namespace csharp NetWork.Auto

struct SCRoomList
{
	1: list<common.RoomInfo> roomList
}