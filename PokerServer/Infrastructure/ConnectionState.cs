using System;

namespace PokerServer
{
	public enum ConnectionState
	{
		NotConnected,
		Preauthentication,
		Keytransfer,
		Authenticated
	}
}

