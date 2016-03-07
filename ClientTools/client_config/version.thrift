namespace csharp Config
namespace java com.kaixin001.hero.client.upload.tools.auto

enum DownloadAssetType
{
	ConfigByte,
	ConfigText,
	Image,
	Audio,
	AssetBundle,
}
struct VersionConfigElement
{
	/** path + name */
	1: string name
	2: string sign
	3: DownloadAssetType type
}

struct VersionConfig
{	
	1: list<VersionConfigElement> versionList
}